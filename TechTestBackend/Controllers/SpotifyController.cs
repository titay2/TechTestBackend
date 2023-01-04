using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MessagePack.Internal;

namespace TechTestBackend.Controllers;

[ApiController]
[Route("api/spotify")]
public class SpotifyController : ControllerBase
{
    //constructors are obsolete should not be used anymore
    // public SpotifyController()
    // {
    // }

    [HttpGet]
    [Route("searchTracks")]
    public IActionResult SearchTracks(string name)
    {
        try
        {        
            // TODO: Implement this method
            object trak = SpotifyHelper.GetTracks(name);

            return Ok(trak);
        }
        catch (Exception e)
        {
            // this is the best practice for not leaking error details
            return NotFound();
        }
    }

    [HttpPost]
    [Route("like")]
    public IActionResult Like(string id)
    {
        object storage = HttpContext.RequestServices.GetService(typeof(SongstorageContext));
        
        var track = SpotifyHelper.GetTrack(id); //check if trak exists
        if(track.Id == null || SpotifyId(id) == false)
        {
            return StatusCode(400);
        }

        var song = new Soptifysong(); //create new song
        song.Id = id;
        song.Name = track.Name;

        try
        {
            //crashes sometimes for some reason
            // we   have to look into this
            ((SongstorageContext)storage).Songs.Add(song);
            
            ((SongstorageContext)storage).SaveChanges();
        }
        catch (Exception e)
        {
            // not sure if this is the best way to handle this
            return Ok();
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("removeLike")]
    public IActionResult RemoveLike(string id)
    {
        object storage = HttpContext.RequestServices.GetService(typeof(SongstorageContext));
        
        var track = SpotifyHelper.GetTrack(id);
        if(track.Id == null || SpotifyId(id) == false)
        {
            return StatusCode(400); // bad request wrong id not existing in spotify
        }

        var song = new Soptifysong();
        song.Id = id;
        
        try
        {
            ((SongstorageContext)storage).Songs.Remove(song); // this is not working every tume
            ((SongstorageContext)storage).SaveChanges();
        }
        catch (Exception e)
        {
            // we should probably log this
            return Ok();
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("listLiked")]
    public IActionResult ListLiked()
    {
        object storage = HttpContext.RequestServices.GetService(typeof(SongstorageContext));

        int songsnumber = ((SongstorageContext)storage).Songs.Count();
        List<Soptifysong> songs = new List<Soptifysong>(); //((SongstorageContext)storage).Songs.ToList();

        if (songsnumber > 0)
        {
            for (int i = 0; i <= songsnumber - 1; i++)
            {
                string songid = ((SongstorageContext)HttpContext.RequestServices.GetService(typeof(SongstorageContext))).Songs.ToList()[i].Id;
            
                var track = SpotifyHelper.GetTrack(songid);
                if(track.Id == null)
                {
                    // TODO: remove song from database, but not sure how
                }
                else
                {
                    // not working for some reason so we have to do the check manually for now
                    // if(SongExists(track.Id) == false)
                    
                    int numerofsong = songs.Count();
                    for (int num = 0; num <= numerofsong; num++)
                    {
                        try
                        {
                            if(songs[num].Id == songid)
                            {
                                break;
                            }
                            else if(num == numerofsong - 1)
                            {

                                for (int namenum = 0; namenum < numerofsong; namenum++)
                                {
                                    if(songs[namenum].Name == track.Name)
                                    {
                                        break; // we dont want to add the same song twice
                                        //does this break work?
                                    }
                                    else if(namenum == numerofsong - 1)
                                    {
                                        songs.Add(((SongstorageContext)storage).Songs.ToList()[i]);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            // something went wrong, but it's not important
                            songs.Add(((SongstorageContext)storage).Songs.ToList()[i]);
                        }
                    }
                }
            }
        }

        //save the changes, just in case
        ((SongstorageContext)storage).SaveChanges();
        
        return Ok(songs);
    }
    
    private bool SongExists(string id)
    {
        return ((SongstorageContext)HttpContext.RequestServices.GetService(typeof(SongstorageContext))).Songs.First(e => e.Id == id) != null;
    }
    
    private static bool SpotifyId(object id)
    {
        return id.ToString().Length == 22;
    }
}
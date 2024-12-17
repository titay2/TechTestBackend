
namespace TechTestBackend
{
    public class ApiMaker(SongstorageContext _storage) : IApiMarker
    {
        public void Add(Soptifysong song)
        {
            if (_storage.Songs.Any(s => s.Id == song.Id))
            {
                throw new Exception("Song already exists!");
            }

            _storage.Songs.Add(song);
            _storage.SaveChanges();
        }

        public List<Soptifysong> GetAll()
        {
            return _storage.Songs.ToList();
        }

        public void Remove(string id)
        {
            var track = _storage.Songs.FirstOrDefault(s => s.Id == id);
            if (track != null)
            {
                _storage.Remove(track);
                _storage.SaveChanges();
            }
            else
            {
                throw new Exception("Song is not in liked list");
            }
        }

        public void RemoveRange(List<Soptifysong> spotifysongs)
        {
            if (spotifysongs.Any())
            {
                _storage.Songs.RemoveRange(spotifysongs);
            }
        }
    }
}

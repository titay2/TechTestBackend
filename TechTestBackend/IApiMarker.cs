namespace TechTestBackend;

public interface IApiMarker
{
    List<Soptifysong> GetAll();
    void Add(Soptifysong song);
    void Remove(string id);
    void RemoveRange(List<Soptifysong> spotifysongs);
}
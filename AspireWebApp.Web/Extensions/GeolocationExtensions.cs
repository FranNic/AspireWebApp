namespace AspireWebApp.Web.Extensions;

using Darnton.Blazor.DeviceInterop.Geolocation;
using Darnton.Blazor.Leaflet.LeafletMap;

public static class GeolocationExtensions
{
    public static LatLng ToLeafletLatLng(this GeolocationPosition position)
    {
        GeolocationCoordinates coords = position.Coords;
        return new LatLng(coords.Latitude, coords.Longitude);
    }
    
    public enum MapZoomLevel
    {
        World = 2,
        Continent = 4,
        Country = 6,
        City = 12,
        Street = 15,
        Building = 18
    }

    public static int ToInt(this MapZoomLevel zoomLevel)
    {
        return (int)zoomLevel;
    }
}

using System.Linq;
using System.Threading.Tasks;

using Android.Locations;

using Xamarin.Forms;

using DemoDS.Models;
using DemoDS.Interfaces;

[assembly: Dependency(typeof(DemoDS.Droid.Dependency.ReverseGeocodeAndroid))]
namespace DemoDS.Droid.Dependency
{
    public class ReverseGeocodeAndroid : IReverseGeocode
    {
        public async Task<LocationAddress> GetLocationAddress(double latitude, double longitude)
        {
            var geoCoder = new Geocoder(Android.App.Application.Context);
            var addresses = await geoCoder.GetFromLocationAsync(latitude, longitude, 1);

            if (addresses.Any())
            {
                var address = addresses.First();

                var location = new LocationAddress()
                {
                    Name = address.FeatureName,
                    City = address.Locality,
                    Province = address.AdminArea,
                    ZipCode = address.PostalCode,
                    CountryCode=address.CountryCode.ToLower(),
                    Country = $"{address.CountryName} ({address.CountryCode})",
                    Address = address.MaxAddressLineIndex > 0 ? address.GetAddressLine(0) : string.Empty
                };

                return location;
            }

            return null;
        }
    }
}
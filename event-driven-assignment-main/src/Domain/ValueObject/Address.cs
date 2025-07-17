using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ValueObject
{
    public class Address
    {
        public Address(string streetName, string streetNumber, string subLocality, string locality,
                string region, string country, string postalCode, string regionCode, string googlePlaceId){
                    this.StreetName = streetName;
                    this.StreetNumber =streetNumber;
                    this.SubLocality = subLocality;
                    this.Locality  = locality;
                    this.Region = region;
                    this.Country = country;
                    this.PostalCode = postalCode;
                    this.RegionCode = regionCode;
                    this.GooglePlaceId = googlePlaceId;
                }
        [ForeignKey("Store")]
        public long StoreId { get; private set; }
        public string StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string SubLocality { get; private set; }
        public string Locality { get; private set; }
        public string Region { get; private set; }
        public string RegionCode { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string GooglePlaceId { get; private set; }
        [NotMapped]
        public string FormattedAddress => this.ToString();

        public override string ToString()
            => $"{this.StreetNumber}, {this.StreetName}, {this.Locality}, {this.Country}, {this.PostalCode}";

        public void UpdateAddressDetails(string streetNumber, string streetName, string subLocality, string locality, string region,
                string postalCode, string regionCode, string country, string googlePlaceId)
        {
            this.StreetNumber = streetNumber;
            this.StreetName = streetName;
            this.SubLocality = subLocality;
            this.Locality = locality;
            this.Region = region;
            this.PostalCode = postalCode;
            this.RegionCode = regionCode;
            this.Country = country;
            this.GooglePlaceId = googlePlaceId;
        }

        public void UpdateGooglePlaceId(string googleplaceId) =>  this.GooglePlaceId = googleplaceId;
    }
}
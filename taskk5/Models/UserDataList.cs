using Bogus;

namespace taskk5.Models
{
    public class UserDataList
    {
        public List<UserData> userDatas;
        public UserDataList(int count, int seed, string locale, string callingCode)
        {
            userDatas = new List<UserData>();
            userDatas = GenerateFakeUserData(count, seed, locale, callingCode); 
        }

        public static List<UserData> GenerateFakeUserData(int count, int seed, string locale, string callingCode)
        {
            var faker = new Bogus.Faker<UserData>(locale)
                .RuleFor(u => u.Identifier, f => f.Random.AlphaNumeric(10))
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber(callingCode));
            faker.UseSeed(seed);
            var addressFormats = new List<Func<Bogus.Faker, string>>
            {
                (f) => f.Address.State() + ", "+ f.Address.City() + ", " + ", "+ f.Address.StreetSuffix() + ", "+f.Address.StreetAddress(),
                (f) => f.Address.StreetAddress(),
                (f) => f.Address.City() + ", " + f.Address.StreetName() + ", " + f.Address.BuildingNumber() + ", "+ f.Address.SecondaryAddress(),
            };

            faker.RuleFor(u => u.Address, f =>
            {
                var randomIndex = f.Random.Number(0, addressFormats.Count - 1);
                return addressFormats[randomIndex](f);
            });
            

            return faker.Generate(count);
        }
    }
}

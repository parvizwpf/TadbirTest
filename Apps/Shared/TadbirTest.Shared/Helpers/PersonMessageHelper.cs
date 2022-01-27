using System;

namespace TadbirTest.Shared.Helpers
{
    public class PersonMessageHelper
    {
        public static PersonMessage GetPersonMessage()
        {
            return new PersonMessage
            {
                Age = GenerateAge(),
                FirstName = GenerateName(6),
                LastName = GenerateName(8)
            };
        }

        private static int GenerateAge()
        {
            Random rand = new Random();
            return rand.Next(11, 99);
        }

        private static string GenerateName(int len)
        {
            Random rand = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[rand.Next(consonants.Length)].ToUpper();
            Name += vowels[rand.Next(vowels.Length)];
            int count = 2;
            while (count < len)
            {
                Name += consonants[rand.Next(consonants.Length)];
                count++;
                Name += vowels[rand.Next(vowels.Length)];
                count++;
            }

            return Name;


        }
    }
}

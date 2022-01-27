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
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }
    }
}

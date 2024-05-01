using System.Reflection;

namespace taskk5.Models
{
    public class Error
    {
        public static Random random = new Random();
        public int GeneratenumError(double error)
        {
            int randomValue = random.Next(1, 101);
            int finalCountOfErrors = (int)error;
            int fractitionalPart = (int)((error % 1)*100);
            if(randomValue <= fractitionalPart)
            {
                finalCountOfErrors++;
            }
            return finalCountOfErrors;

        }
        public string ErrorDelete(string input)
        {
            if (input.Length <= 2)
            {
                return input;
            }
            int index = random.Next(input.Length);
            return input.Remove(index, 1);

        }

        public char GenerateRandomChar()
        {
            Random random = new Random();
            char randomChar;
            do
            {
                randomChar = (char)random.Next(33, 127); 
            } while (!char.IsLetterOrDigit(randomChar)); 

            return randomChar;
        }

        public string AddRandom(string input)
        {
            if (input.Length >= 15)
            {
                return input;
            }

            Random random = new Random();
            int index = random.Next(input.Length);
            char randomChar = GenerateRandomChar();

            return input.Insert(index, randomChar.ToString());
        }

        public string SwapLetters(string input)
        {
            if (input.Length == 0)
            {
                return input;
            }
            int index = random.Next(input.Length - 1);
            char[] charArray = input.ToCharArray();
            char temp = charArray[index];
            charArray[index] = charArray[index + 1];
            charArray[index + 1] = temp;
            return new string(charArray);
        }

        public string ChooseTypeOfError(string input)
        {
            int typeOfError = random.Next(1, 4);
            switch (typeOfError)
            {
                case 1:
                    {
                        
                        return ErrorDelete(input);
                    }
                case 2:
                    {
                        return AddRandom(input);

                    }
                case 3:
                    {
                        return SwapLetters(input);
                    }
            }
            return input;
        }

        public List<UserData> GenerateErrors(List<UserData> info, int error)
        {
            string[] fields = {"FullName", "Address", "Phone" };
            
            int randomIndex = 0;
            for(int i = 0; i < info.Count; i++)
            {
                for(int j = 0; j <error; j++)
                {
                    int randomValue = random.Next(0, 3);
                    UserData userData = info[i];
                    PropertyInfo property = userData.GetType().GetProperty(fields[randomValue]);
                    object value = property.GetValue(userData);
                    value = ChooseTypeOfError(value.ToString());
                    property.SetValue(userData, value.ToString());
                }
            }
            return info;
        }
    }
}

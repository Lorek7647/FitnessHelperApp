namespace FitnessHelper.Code
{
    class InputValidator
    { 
        public static bool EmptyCheck(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;    
            }
            else
            {
                return false;
            }   
        }
        public static bool EmptyCheck3(string input1, string input2, string input3)
        {
            if (!string.IsNullOrEmpty(input1) && !string.IsNullOrEmpty(input1) && !string.IsNullOrEmpty(input1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int NumberCheck(string input)
        {
            bool success = int.TryParse(input, out int number);
            if (success)
            {
                return number;
            }
            else
            {
                return 0;
            }      
        }
    }
}

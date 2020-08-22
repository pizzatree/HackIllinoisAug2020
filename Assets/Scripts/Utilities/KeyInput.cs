using UnityEngine;

namespace Utilities
{
    public static class KeyInput
    {
        public static char? GetKey(bool letters = false)
        {
            for(var num = '0'; num <= '9'; ++num)
            {
                var numString    = num.ToString();
                var keyNumString = "[" + numString + "]";

                if(Input.GetKeyDown(numString) || Input.GetKeyDown(keyNumString))
                    return num;
            }

            if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
                return '-';

            if(Input.GetKeyDown(KeyCode.Backspace))
                return (char) 0;

            if(letters)
                for(var key = 'a'; key <= 'z'; ++key)
                    if(Input.GetKeyDown(key.ToString()))
                        return key;

            return null;
        }
    }
}
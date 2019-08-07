using AutomationFramework.Tests;
using System;
using System.IO;
using System.Reflection;

class AdditionalFunctions
{
    /// <summary>
    /// PhotoSelection will select an image to upload from the 'Support\Image Bank' folder of the framework.
    /// </summary>
    /// <param name="selection">Select the number of the automation image #.png to use. You can also enter '0' for this field to randomly pick an image</param>
    /// <param name="fileName">Optional filename to upload. If using this the selection field will be ignored.</param>
    public static void PhotoSelection(int selection = 0, string fileName = null)
    {
        string homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Core)).Location);
        string trueFilename;
        
        if (fileName == null)
        {
            int fCount = Directory.GetFiles($"{homeDirectory}\\Support\\Image Bank\\", "automation image*", SearchOption.TopDirectoryOnly).Length;

            if (selection == 0)
            {
                Random random = new Random();
                selection = random.Next(1, (fCount + 1));
            }

            else if (selection > fCount)
            {
                Random random = new Random();
                selection = random.Next(1, (fCount + 1));
            }

            trueFilename = $"automation image {selection}.png";

            /* The above generates the following error with a 3 X .Net dialog
             * $exception	{"Could not load type 'System.Runtime.InteropServices.StandardOleMarshalObject' from assembly 
             * 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.":"System.Runtime.InteropServices.StandardOleMarshalObject"}	System.TypeLoadException
             */
        }

        else
        {
            trueFilename = fileName;
        }
        
        System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\" + trueFilename);
        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
    }
}

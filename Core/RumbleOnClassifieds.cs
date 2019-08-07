using AutomationFramework.Tests;
using System;
using System.IO;
using System.Reflection;


class RumbleOnClassifieds
{
    /// <summary>
    /// Will randomly pic an image to upload. 
    /// </summary>
    /// <param name="selection">0 through 4. Anything larger will randomly pic</param>
    public static void PhotoSelection(int selection)
    {
        string homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Core)).Location);
        int fCount = Directory.GetFiles($"{homeDirectory}\\Support\\Image Bank\\", "*", SearchOption.TopDirectoryOnly).Length;

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

        System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image {selection}.png");
        System.Windows.Forms.SendKeys.SendWait("{ENTER}");

        /* The above generates the following error with a 3 X .Net dialog
         * $exception	{"Could not load type 'System.Runtime.InteropServices.StandardOleMarshalObject' from assembly 
         * 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.":"System.Runtime.InteropServices.StandardOleMarshalObject"}	System.TypeLoadException
         */
    }
}

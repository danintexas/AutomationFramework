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
        Random random = new Random();
        int randomNumber = random.Next(1, 5);

        switch (selection)
        {
            case 1:
                System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image 1.png");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                break;
            case 2:
                System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image 2.png");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                break;
            case 3:
                System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image 3.png");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                break;
            case 4:
                System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image 4.png");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                break;
            default:
                System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\automation image {randomNumber}.png");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                break;
        }
    }
}

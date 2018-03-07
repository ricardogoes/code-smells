using System;
using System.Linq;

namespace CodeSmells.ObjectOrientationAbusers
{
    public class SwitchAbuse
    {
        dynamic SpotLightLine1Textbox;
        dynamic SpotLightLine2Textbox;
        dynamic TagTextBox;
        dynamic CustomTagTextBox;
       
        public void Button_Click()
        {
            int CreativeFormatId = 1;
            switch (MyCreative.CreativeTypeId)
            {
                case (int)CreativeTypes.Text:
                    if (!MyCreative.IsEmailCreativeFormat)
                    {
                        MyCreative.SpotlightLine1 = SpotLightLine1Textbox.Text;
                        MyCreative.SpotlightLine2 = SpotLightLine2Textbox.Text;
                    }
                    else
                        MyCreative.ThirdPartyTags = TagTextBox.Text;
                    break;

                case (int)CreativeTypes.Custom:
                    MyCreative.ThirdPartyServed = 
                        CreativeFormatId == Constants.SPONSOR_EMAIL_CREATIVE_FORMAT_ID ? false : true;

                    MyCreative.ThirdPartyTags = CustomTagTextBox.Text;
                    break;

                case (int)CreativeTypes.Image:
                case (int)CreativeTypes.Flash:
                    MyCreative.CreativeUrl = SaveCreativeFile();
                    break;

                case (int)CreativeTypes.Mixed:
                    MyCreative.CreativeUrl = SaveCreativeFile();
                    MyCreative.ThirdPartyTags = TagTextBox.Text;
                    break;

                case (int)CreativeTypes.None:
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Creative Type ID " + MyCreative.CreativeTypeId +
                                                        " detected.");
            }
        }
  
        private string SaveCreativeFile()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
  
    public class Constants
    {
        public static int SPONSOR_EMAIL_CREATIVE_FORMAT_ID;
    }
  
    public enum CreativeTypes
    {
        Text,
        Custom,
        Image,
        Mixed,
        None,
        Flash
    }
  
    public class MyCreative
    {
        public static int CreativeTypeId { get; set; }
        public static string CreativeUrl { get; set; }
        public static bool IsEmailCreativeFormat { get; set; }
        public static dynamic SpotlightLine1 { get; set; }
        public static dynamic SpotlightLine2 { get; set; }
        public static dynamic ThirdPartyTags { get; set; }
        public static bool ThirdPartyServed { get; set; }
    }
}

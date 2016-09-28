using Foundation;
using System;
using UIKit;

namespace expandableTableView.iOS
{
    public partial class CustomCell : UITableViewCell
    {
        public CustomCell (IntPtr handle) : base (handle)
        {
        }
		partial void CelTextEditEnd(UIKit.UITextField sender)
		{
			Console.WriteLine("hi");
		}

	}
}
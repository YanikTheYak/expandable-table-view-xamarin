using System;
using UIKit;

namespace expandableTableView.iOS
{
	public interface CustomCellDelegate
	{
		void dateWasSelected(string selectedDateString);
	}

	public partial class CustomCell22 : UITableViewCell, IUITextFieldDelegate
	{
		CustomCellDelegate ccDelegate;
		public CustomCell22() : base()
		{
		}
	}
}

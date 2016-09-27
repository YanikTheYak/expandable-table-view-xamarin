using System;
using UIKit;

namespace expandableTableView.iOS
{
	public interface CustomCellDelegate
	{
		void dateWasSelected(string selectedDateString);
	}

	public class CustomCell : UITableViewCell, IUITextFieldDelegate
	{
		CustomCellDelegate ccDelegate;
		public CustomCell() : base()
		{
		}
	}
}

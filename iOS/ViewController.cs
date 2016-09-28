using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

using UIKit;

namespace expandableTableView.iOS
{
	public partial class ViewController : UITableViewController
	{
		ViewControllerDelegate vcDelegate = null;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			vcDelegate = new ViewControllerDelegate(this.TableView );
			this.TableView.Source = vcDelegate;


		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}

	public class ViewControllerDelegate : UITableViewSource, CustomCellDelegate
	{		
		UITableView tblExpandable;

		NSMutableArray cellDescriptors;
		List<List<int>> visibleRowsPerSection = new List<List<int>>();


		public ViewControllerDelegate(UITableView tableView)
		{
			tblExpandable = tableView;
			configureTableView();

			loadCellDescriptors();
			Console.WriteLine(cellDescriptors.ToString());
		}

		private void configureTableView()
		{
	//		tblExpandable.Delegate = (IUITableViewDelegate)this;
	//		tblExpandable.DataSource = (IUITableViewDataSource)this;
			tblExpandable.TableFooterView = new UIView();

			tblExpandable.RegisterNibForCellReuse(UINib.FromName("NormalCell", null), "idCellNormal");
			tblExpandable.RegisterNibForCellReuse(UINib.FromName("TextfieldCell", null), "idCellTextfield");
			tblExpandable.RegisterNibForCellReuse(UINib.FromName("DatePickerCell", null), "idCellDatePicker");
			tblExpandable.RegisterNibForCellReuse(UINib.FromName("SwitchCell", null), "idCellSwitch");
			tblExpandable.RegisterNibForCellReuse(UINib.FromName("ValuePickerCell", null), "idCellValuePicker");
			tblExpandable.RegisterNibForCellReuse(UINib.FromName("SliderCell", null), "idCellSlider");
		}

		private void loadCellDescriptors()
		{
			string path;

			path = NSBundle.MainBundle.BundlePath;
			Console.WriteLine(path);
			path += @"/CellDescriptor.plist";

			cellDescriptors = NSMutableArray.FromFile(path);

			getIndicesOfVisibleRows(); 

			tblExpandable.ReloadData();
		}

		private void getIndicesOfVisibleRows()
		{
			visibleRowsPerSection.Clear();

			for (nuint i = 0; i < cellDescriptors.Count; i++)
			{
				var currentSectionCells = cellDescriptors.GetItem<NSMutableArray>(i);
				List<int> visibleRows = new List<int>();

				for (nuint j = 0; j < currentSectionCells.Count; j++)
				{
					var currentSectionRow = currentSectionCells.GetItem<NSMutableDictionary>(j);

					if (currentSectionRow.ContainsKey(new NSString("isVisible")) &&
					    currentSectionRow.ValueForKey(new NSString("isVisible")).ToString() == "1")
					{
						visibleRows.Add((int)j);
					}
					    
				}
				visibleRowsPerSection.Add(visibleRows);

			}

		}

		private NSMutableDictionary getCellDescriptorForIndexPath(NSIndexPath indexPath) 
		{
			var indexOfVisibleRow = visibleRowsPerSection[indexPath.Section][indexPath.Row];

			NSMutableDictionary cellDescriptor = cellDescriptors.GetItem<NSMutableArray>((System.nuint)indexPath.Section).GetItem<NSMutableDictionary>((System.nuint)indexOfVisibleRow);

			return cellDescriptor;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if (cellDescriptors != null)
			{
				return (int)cellDescriptors.Count;
			}
			else
			{
				return 0;
			}
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return visibleRowsPerSection[(int)section].Count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			switch ((int) section) 
			{
				case 0:
					return "Personal";	
				case 1:
					return "Preferences";
				default:
					return "Work Experience";
			}
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			NSMutableDictionary currentCellDescriptor = getCellDescriptorForIndexPath(indexPath);
			string cellIdentifier = currentCellDescriptor.ValueForKey(new NSString("cellIdentifier")).ToString();
			UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier, indexPath);

			Console.WriteLine(cell.GetType().ToString());

			switch (cellIdentifier)
			{
				case "idCellNormal":
					{
						break;
					}
				case "idCellTextfield":
					{
						break;
					}
				case "idCellSwitch":
					{
						break;
					}
				case "idCellValuePicker":
					{
						break;
					}
				case "idCellSlider":
					{
						break;
					}
				default:
					break;
			}
			//cell.Delegate = self; // TODO: Fix this?


			return cell;
		}


/* TODO INSERT THIS ABOVE
        
        if currentCellDescriptor["cellIdentifier"] as! String == "idCellNormal" {
            if let primaryTitle = currentCellDescriptor["primaryTitle"] {

				cell.textLabel?.text = primaryTitle as? String
	}
            
            if let secondaryTitle = currentCellDescriptor["secondaryTitle"] {
                cell.detailTextLabel?.text = secondaryTitle as? String

			}
        }
        else if currentCellDescriptor["cellIdentifier"] as! String == "idCellTextfield" {
            cell.textField.placeholder = currentCellDescriptor["primaryTitle"] as? String
        }
        else if currentCellDescriptor["cellIdentifier"] as! String == "idCellSwitch" {
            cell.lblSwitchLabel.text = currentCellDescriptor["primaryTitle"] as? String


			let value = currentCellDescriptor["value"] as? String

			cell.swMaritalStatus.on = (value == "true") ? true : false
        }
        else if currentCellDescriptor["cellIdentifier"] as! String == "idCellValuePicker" {
            cell.textLabel?.text = currentCellDescriptor["primaryTitle"] as? String
        }
        else if currentCellDescriptor["cellIdentifier"] as! String == "idCellSlider" {
            let value = currentCellDescriptor["value"] as! String
			cell.slExperienceLevel.value = (value as NSString).floatValue
        }
        
    
    }

*/

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			NSMutableDictionary currentCellDescriptor = getCellDescriptorForIndexPath(indexPath);
			string cellIdentifier = currentCellDescriptor.ValueForKey(new NSString("cellIdentifier")).ToString();

			switch (cellIdentifier)
			{
		        case "idCellNormal":
					return 60.0f;
		               
		        case "idCellDatePicker":
					return 270.0f;
		   
				default:
					return 44.0f;   
			}
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var indexOfTappedRow = visibleRowsPerSection[indexPath.Section][indexPath.Row];
			NSMutableDictionary currentCellDescriptor = (NSMutableDictionary)getCellDescriptorForIndexPath(indexPath);

			string isExpandable = currentCellDescriptor.ValueForKey(new NSString("isExpandable")).ToString();
			string isExpanded = currentCellDescriptor.ValueForKey(new NSString("isExpanded")).ToString();

			if (isExpandable == "1")
			{
				bool shouldExpandAndShowSubRows = false;

				if (isExpanded == "0")
				{
					// In this case the cell should expand.
					shouldExpandAndShowSubRows = true;

				}
				int x = shouldExpandAndShowSubRows ? 1 : 0;
				currentCellDescriptor.SetValueForKey(new NSString(x.ToString()), new NSString("isExpanded"));

				int additionalRows = (int)(NSNumber)currentCellDescriptor.ValueForKey(new NSString("additionalRows"));

				for (int i = indexOfTappedRow + 1; i <= indexOfTappedRow + additionalRows; i++)
				{
					NSMutableDictionary cellDescriptor = (NSMutableDictionary)cellDescriptors.GetItem<NSMutableArray>((System.nuint)indexPath.Section).GetItem<NSMutableDictionary>((System.nuint)i);
					cellDescriptor.SetValueForKey(new NSString(x.ToString()), new NSString("isVisible"));
				}
			}
			else
			{
				string cellIdentifier = currentCellDescriptor.ValueForKey(new NSString("cellIdentifier")).ToString();

				if (cellIdentifier == "idCellValuePicker")
				{
					int indexOfParentCell = 0;

					for (int i = indexOfTappedRow - 1; i >= 0; i -= 1)
					{
						NSMutableDictionary cellDescriptor = cellDescriptors.GetItem<NSMutableArray>((System.nuint)indexPath.Section).GetItem<NSMutableDictionary>((System.nuint)i);
						if (cellDescriptor.ValueForKey(new NSString("isExpandable")).ToString() == "Yes")
						{
							indexOfParentCell = i;
							break;
						}
					}

					NSMutableDictionary cellDescriptor2 = cellDescriptors.GetItem<NSMutableArray>((System.nuint)indexPath.Section).GetItem<NSMutableDictionary>((System.nuint)indexOfParentCell);
					CustomCell cell = (CustomCell)tblExpandable.CellAt(indexPath);

					cellDescriptor2.SetValueForKey(new NSString(cell.TextLabel.Text), new NSString("primaryTitle"));
					cellDescriptor2.SetValueForKey(new NSString("0"), new NSString("isExpanded"));
					int additionalRows = (int)(NSNumber)cellDescriptor2.ValueForKey(new NSString("additionalRows"));

					for (int i = (indexOfParentCell + 1); i <= (indexOfParentCell + additionalRows); i++)
					{
						NSMutableDictionary cellDescriptor3 = cellDescriptors.GetItem<NSMutableArray>((System.nuint)indexPath.Section).GetItem<NSMutableDictionary>((System.nuint)i);
						cellDescriptor3.SetValueForKey(new NSString("0"), new NSString("isVisible"));
					}
				}

			}
			getIndicesOfVisibleRows();

			tblExpandable.ReloadSections(new NSIndexSet((nuint)indexPath.Section), UITableViewRowAnimation.Fade);
		}


		void CustomCellDelegate.dateWasSelected(string selectedDateString) { }
	}
}

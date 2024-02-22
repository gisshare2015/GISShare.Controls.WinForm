using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IComboSearchBoxItem : IComboBoxItem
    {
        bool ShowSearchBox { get; set; }
        bool SearchBoxIsTop { get; set; }
        List<View.ViewItem> Items { get; }
        void UpdateItems();
    }
}

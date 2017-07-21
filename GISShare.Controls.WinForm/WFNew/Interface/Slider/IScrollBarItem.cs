using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IScrollBarItem : IBaseItem
    {
        System.Windows.Forms.Orientation eOrientation { get; set; }

        int Step { get;set; }

        int Value { get;set; }

        int Maximum { get;set; }

        int Minimum { get; set;}

        int Percentage { get; }

        int ScrollButtonSize { get; }
        
        int MinusPlusButtonSize { get; }

        Rectangle DrawRectangle { get; }

        Rectangle MinusButtonRectangle { get; }

        Rectangle PlusButtonRectangle { get; }

        Rectangle ScrollAreaRectangle { get; }

        Rectangle ScrollValueAreaRectangle { get; }

        Rectangle ScrollButtonRectangle { get; }
        
        int GetEffectiveValue();

        int GetValueFromPoint(Point point);
    }
}

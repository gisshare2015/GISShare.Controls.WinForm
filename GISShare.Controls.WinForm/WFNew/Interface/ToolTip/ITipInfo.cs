﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITipInfo
    {
        Image TitleImage { get; }
        string TipInfoText { get; }
        string TitleText { get; }
    }
}

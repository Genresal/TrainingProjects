﻿using System.ComponentModel;

namespace BlazorServerTest.Core.Commons.Enums;

public enum SortOrder
{
    [Description("none")] None,
    [Description("ascending")] Ascending,
    [Description("descending")] Descending,
}

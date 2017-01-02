﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;

namespace WpfViewModelBasics.Core.Interfaces.Services.Command
{
    public interface IFriendCommandService
    {
        Task AddFriend(Friend friend);
        Task UpdateFriend(Friend friend);
    }
}
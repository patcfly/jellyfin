#nullable disable
#pragma warning disable CS1591

using System;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Enums;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Sorting;
using MediaBrowser.Model.Querying;

namespace Emby.Server.Implementations.Sorting
{
    public class DateLastMediaAddedComparer : IUserBaseItemComparer
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        /// <value>The user manager.</value>
        public IUserManager UserManager { get; set; }

        /// <summary>
        /// Gets or sets the user data repository.
        /// </summary>
        /// <value>The user data repository.</value>
        public IUserDataManager UserDataRepository { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public ItemSortBy Type => ItemSortBy.DateLastContentAdded;

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Int32.</returns>
        public int Compare(BaseItem x, BaseItem y)
        {
            return GetDate(x).CompareTo(GetDate(y));
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>DateTime.</returns>
        private static DateTime GetDate(BaseItem x)
        {
            if (x is Folder folder)
            {
                if (folder.DateLastMediaAdded.HasValue)
                {
                    return folder.DateLastMediaAdded.Value;
                }
            }

            // For non-folder items, use the creation date or another appropriate date property.
            if (x is MediaItem mediaItem && mediaItem.DateCreated.HasValue)
            {
                return mediaItem.DateCreated.Value;
            }

            // Return DateTime.MinValue only if no date information is available.
            return DateTime.MinValue;
        }
    }
}

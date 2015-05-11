﻿using Umbraco.Core;

namespace Umbraco.Web.PublishedCache.XmlPublishedCache
{
    /// <summary>
    /// Provides caches (content and media).
    /// </summary>
    class PublishedCaches : IPublishedCaches
    {
        private readonly PublishedContentCache _contentCache;
        private readonly PublishedMediaCache _mediaCache;
        private readonly PublishedMemberCache _memberCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedCaches"/> class with a content cache
        /// and a media cache.
        /// </summary>
        public PublishedCaches(PublishedContentCache contentCache, PublishedMediaCache mediaCache, PublishedMemberCache memberCache)
        {
            _contentCache = contentCache;
            _mediaCache = mediaCache;
            _memberCache = memberCache;
        }

        /// <summary>
        /// Gets the <see cref="IPublishedContentCache"/>.
        /// </summary>
        public IPublishedContentCache ContentCache
        {
            get { return _contentCache; }
        }

        /// <summary>
        /// Gets the <see cref="IPublishedMediaCache"/>.
        /// </summary>
        public IPublishedMediaCache MediaCache
        {
            get { return _mediaCache; }
        }

        /// <summary>
        /// Gets the <see cref="IPublishedMemberCache"/>.
        /// </summary>
        public IPublishedMemberCache MemberCache
        {
            get { return _memberCache; }
        }

        public static void ResyncCurrent()
        {
            if (PublishedCachesServiceResolver.HasCurrent == false) return;
            var service = PublishedCachesServiceResolver.Current.Service as PublishedCachesService;
            if (service == null) return;
            var facade = service.GetPublishedCaches() as PublishedCaches;
            if (facade == null) return;
            facade._contentCache.Resync();
            facade._mediaCache.Resync();

            // note: the media cache does not resync because it is fully sync
            // note: the member cache does not resync...
            // not very consistent but we're not trying to fix it at that point
        }
    }
}

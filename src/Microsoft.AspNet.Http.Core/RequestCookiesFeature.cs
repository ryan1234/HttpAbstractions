// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.FeatureModel;
using Microsoft.AspNet.Http.Core.Collections;
using Microsoft.AspNet.Http.Core.Infrastructure;
using Microsoft.Framework.Internal;
using Microsoft.Net.Http.Headers;

namespace Microsoft.AspNet.Http.Core
{
    public class RequestCookiesFeature : IRequestCookiesFeature
    {
        private readonly IFeatureCollection _features;
        private readonly FeatureReference<IHttpRequestFeature> _request = FeatureReference<IHttpRequestFeature>.Default;
        private string _cookiesHeader;
        private RequestCookiesCollection _cookiesCollection;
        private IReadableStringCollection _cookies;

        public RequestCookiesFeature([NotNull] IDictionary<string, string[]> cookies)
            : this (new ReadableStringCollection(cookies))
        {
        }

        public RequestCookiesFeature([NotNull] IReadableStringCollection cookies)
        {
            _cookies = cookies;
        }

        public RequestCookiesFeature([NotNull] IFeatureCollection features)
        {
            _features = features;
        }

        public IReadableStringCollection Cookies
        {
            get
            {
                if (_features == null)
                {
                    return _cookies;
                }

                var headers = _request.Fetch(_features).Headers;
                string[] values;
                if (!headers.TryGetValue(HeaderNames.Cookie, out values))
                {
                    values = new string[0];
                }
                var cookiesHeader = string.Join(", ", values);

                if (_cookiesCollection == null)
                {
                    _cookiesHeader = cookiesHeader;
                    _cookiesCollection = new RequestCookiesCollection();
                    _cookiesCollection.Reparse(values);
                }
                else if (!string.Equals(_cookiesHeader, cookiesHeader, StringComparison.Ordinal))
                {
                    _cookiesHeader = cookiesHeader;
                    _cookiesCollection.Reparse(values);
                }

                return _cookiesCollection;
            }
        }
    }
}
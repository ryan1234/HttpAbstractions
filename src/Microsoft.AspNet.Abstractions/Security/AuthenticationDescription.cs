// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.HttpFeature.Security;

namespace Microsoft.AspNet.Abstractions.Security
{
    /// <summary>
    /// Contains information describing an authentication provider.
    /// </summary>
    public class AuthenticationDescription
    {
        private const string CaptionPropertyKey = "Caption";
        private const string AuthenticationTypePropertyKey = "AuthenticationType";

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationDescription"/> class
        /// </summary>
        public AuthenticationDescription()
        {
            Dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationDescription"/> class
        /// </summary>
        /// <param name="properties"></param>
        public AuthenticationDescription(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            Dictionary = properties;
        }

        /// <summary>
        /// Contains metadata about the authentication provider.
        /// </summary>
        public IDictionary<string, object> Dictionary { get; private set; }

        /// <summary>
        /// Gets or sets the name used to reference the authentication middleware instance.
        /// </summary>
        public string AuthenticationType
        {
            get { return GetString(AuthenticationTypePropertyKey); }
            set { Dictionary[AuthenticationTypePropertyKey] = value; }
        }

        /// <summary>
        /// Gets or sets the display name for the authentication provider.
        /// </summary>
        public string Caption
        {
            get { return GetString(CaptionPropertyKey); }
            set { Dictionary[CaptionPropertyKey] = value; }
        }

        private string GetString(string name)
        {
            object value;
            if (Dictionary.TryGetValue(name, out value))
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}
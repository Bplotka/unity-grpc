#region Copyright notice and license

// Copyright 2015, Google Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//     * Neither the name of Google Inc. nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Grpc.Core.Internal
{
    /// <summary>
    /// Overrides the content of default SSL roots.
    /// </summary>
    internal static class DefaultSslRootsOverride
    {
        // const string RootsPemResourceName = "Grpc.Core.roots.pem";
        static object staticLock = new object();

        /// <summary>
        /// Overrides C core's default roots with roots.pem loaded as embedded resource.
        /// </summary>
        public static void Override(NativeMethods native)
        {
            lock (staticLock)
            {
                //var stream = typeof(DefaultSslRootsOverride).GetTypeInfo().Assembly.GetManifestResourceStream(RootsPemResourceName);
                //if (stream == null)
                //{
                //    throw new IOException(string.Format("Error loading the embedded resource \"{0}\"", RootsPemResourceName));   
                //}
                //using (var streamReader = new StreamReader(stream))
                //{
                //    var pemRootCerts = streamReader.ReadToEnd();
                //    native.grpcsharp_override_default_ssl_roots(pemRootCerts);
                //}
            }
        }
    }
}
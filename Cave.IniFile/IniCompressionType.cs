﻿using System;

namespace Cave
{
    /// <summary>
    /// Available ini compression types.
    /// </summary>
    [Obsolete("Update to nuget package Cave.IO!")]
    public enum IniCompressionType
    {
        /// <summary>
        /// No compression, no encryption
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Compression using deflate
        /// </summary>
        Deflate = 0x01,

        /// <summary>
        /// Compression using gzip
        /// </summary>
        GZip = 0x02,
    }
}

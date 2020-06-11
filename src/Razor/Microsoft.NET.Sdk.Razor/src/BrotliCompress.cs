// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using Microsoft.Build.Framework;

namespace Microsoft.AspNetCore.Razor.Tasks
{
    public class BrotliCompress : DotNetToolTask
    {
        [Required]
        public ITaskItem[] FilesToCompress { get; set; }

        public string CompressionLevel { get; set; }

        internal override string Command => "brotli";

        protected override string GenerateResponseFileCommands()
        {
            var builder = new StringBuilder();

            builder.AppendLine(Command);

            if (!string.IsNullOrEmpty(CompressionLevel))
            {
                builder.AppendLine("-c");
                builder.AppendLine(CompressionLevel);
            }

            for (var i = 0; i < FilesToCompress.Length; i++)
            {
                var input = FilesToCompress[i];
                builder.AppendLine("-s");
                builder.AppendLine(input.GetMetadata("FullPath"));

                builder.AppendLine("-o");
                builder.AppendLine(input.GetMetadata("TargetPath"));
            }

            return builder.ToString();
        }
    }
}

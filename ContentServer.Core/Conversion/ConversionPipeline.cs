﻿using ContentServer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public class ConversionStep
    {
        private static readonly Regex inputPattern = new Regex("^[a-z,0-9]{1,32}$");
        public ConversionStep(ConversionDefinition conversion, params string[] input)
        {
            Conversion = conversion;
            this.Input = new HashSet<string>(input);

            if (this.Input.Count != input.Length)
            {
                throw new ArgumentException(nameof(input), "Duplicate input aliases");
            }

            foreach (string item in input)
            {
                if (!inputPattern.IsMatch(item))
                {
                    throw new ArgumentException(nameof(input), $"input value {item} not match {inputPattern} pattern");
                }
            }

            this.Output = HashHelper.HashMd5(this.Conversion.Hash, this.Input);
        }
        public IReadOnlySet<string> Input { get; }
        
        public ConversionDefinition Conversion { get; }

        public string Output { get; }        
    }
    public class ConversionPipeline
    {
        public ConversionPipeline(Dictionary<int, ConversionStep> steps)
        {
            this.Steps = steps;
        }
        public IReadOnlyDictionary<int, ConversionStep> Steps { get; }
    }
}
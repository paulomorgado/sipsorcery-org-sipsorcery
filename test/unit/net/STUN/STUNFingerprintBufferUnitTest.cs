//-----------------------------------------------------------------------------
// Filename: STUNFingerprintBufferUnitTest.cs
//
// Description: Unit tests for ParseSTUNMessage fingerprint validation with
// oversized buffers.
//
// License:
// BSD 3-Clause "New" or "Revised" License, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using Xunit;

namespace SIPSorcery.Net.UnitTests
{
    [Trait("Category", "unit")]
    public class STUNFingerprintBufferUnitTest
    {
        /// <summary>
        /// Verifies that fingerprint validation works when the buffer is exactly
        /// the size of the STUN message (baseline).
        /// </summary>
        [Fact]
        public void FingerprintValidWithExactBuffer()
        {
            string key = "SKYKPPYLTZOAVCLTGHDUODANRKSPOVQVKXJULOGG";

            var msg = new STUNMessage(STUNMessageTypesEnum.BindingRequest);
            msg.Header.TransactionId = Encoding.ASCII.GetBytes("abcdefghijkl");
            msg.AddUsernameAttribute("xxxx:yyyy");
            msg.Attributes.Add(new STUNAttribute(STUNAttributeTypesEnum.Priority, BitConverter.GetBytes(1U)));

            var exact = new byte[msg.GetByteBufferSizeStringKey(key, true)];
            msg.WriteToBufferStringKey(exact, key, true);
            var parsed = STUNMessage.ParseSTUNMessage(exact);

            Assert.True(parsed.isFingerprintValid);
            Assert.True(parsed.CheckIntegrity(Encoding.UTF8.GetBytes(key)));
        }
    }
}

// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 11/23/2022.
// Scope of modification:
//   - Code adaptation to project requirements.

//------------------------------------------------------------------------------------
// Copyright 2015 Google Inc. All Rights Reserved. 
//
// Distributed under MIT license.
// See file LICENSE for detail or copy at https://opensource.org/licenses/MIT
//------------------------------------------------------------------------------------

namespace SharpVectors.Compressions.Brotli
{
	/// <summary>Byte-to-int conversion magic.</summary>
	internal sealed class IntReader
	{
		private byte[] byteBuffer;

		private int[] intBuffer;

		internal static void Init(IntReader ir, byte[] byteBuffer, int[] intBuffer)
		{
			ir.byteBuffer = byteBuffer;
			ir.intBuffer = intBuffer;
		}

		/// <summary>Translates bytes to ints.</summary>
		/// <remarks>
		/// Translates bytes to ints.
		/// NB: intLen == 4 * byteSize!
		/// NB: intLen should be less or equal to intBuffer length.
		/// </remarks>
		internal static void Convert(IntReader ir, int intLen)
		{
			for (int i = 0; i < intLen; ++i)
            {
                ir.intBuffer[i] = (ir.byteBuffer[i * 4] & 0xFF) |
                    ((ir.byteBuffer[(i * 4) + 1] & 0xFF) << 8) |
                    ((ir.byteBuffer[(i * 4) + 2] & 0xFF) << 16) |
                    ((ir.byteBuffer[(i * 4) + 3] & 0xFF) << 24);
            }
		}
	}
}

// ///-----------------------------------------------------------------
// ///   Class:          TestTest
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 11.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
namespace LibDataService
{
    public class TestTest
    {
		[TestFixture]
        public TestTest ()
        {
        }
		public void blub1 ()
		{
			Assert.True ( true );
		}

		[Test]
		public void Blub2 ()
		{
			Assert.False ( true );
		}

		[Test]
		[Ignore ( "another time" )]
		public void Blub3 ()
		{
			Assert.True ( false );
		}
    }
}

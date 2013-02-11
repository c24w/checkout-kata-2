using NUnit.Framework;

namespace Checkout_Kata_2
{
	[TestFixture]
	public class CheckoutKata2
	{
		[Test]
		public void Checkout_total_should_be_zero()
		{
			var checkout = new Checkout();
			Assert.That(checkout.Total, Is.EqualTo(0));
		}

		[TestCase(1, 50)]
		[TestCase(2, 100)]
		[TestCase(3, 130)]
		public void Checkout_scan_number_of_item_a_should_return_the_expected_total(int numberOfAs, int expectedTotal)
		{
			var checkout = new Checkout();
			var basket = "";
			for (var i = 0; i < numberOfAs; i++)
				basket += 'a';
			checkout.Scan(basket);
			Assert.That(checkout.Total, Is.EqualTo(expectedTotal));
		}
	}

	public class Checkout
	{
		public int Total { get; private set; }

		public void Scan(string basket)
		{
			var items = basket.ToCharArray();
			var numAs = 0;
			foreach (var item in items)
			{
				++numAs;
				Total += 50;
			}
			if (numAs == 3) Total -= 20;
		}
	}
}

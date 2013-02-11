using System.Collections.Generic;
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
		[TestCase(6, 260)]
		public void Checkout_scan_number_of_item_a_should_return_the_expected_total(int numberOfAs, int expectedTotal)
		{
			var checkout = new Checkout();
			var basket = "";
			for (var i = 0; i < numberOfAs; i++)
				basket += 'a';
			checkout.ScanBasket(basket);
			Assert.That(checkout.Total, Is.EqualTo(expectedTotal));
		}

		[TestCase(1, 30)]
		[TestCase(2, 45)]
		[TestCase(4, 90)]
		public void Checkout_scan_number_of_item_b_should_return_the_expected_total(int numberOfBs, int expectedTotal)
		{
			var checkout = new Checkout();
			var basket = "";
			for (var i = 0; i < numberOfBs; i++)
				basket += 'b';
			checkout.ScanBasket(basket);
			Assert.That(checkout.Total, Is.EqualTo(expectedTotal));
		}

		[TestCase("ab", 80)]
		[TestCase("aaab", 160)]
		[TestCase("abbbb", 140)]
		[TestCase("aaabb", 175)]
		public void Checkout_scan_mixed_basket_should_return_the_expected_total(string basket, int expectedTotal)
		{
			var checkout = new Checkout();
			checkout.ScanBasket(basket);
			Assert.That(checkout.Total, Is.EqualTo(expectedTotal));
		}
	}

	public class Checkout
	{
		private int _itemACount;
		private int _itemBCount;
		public int Total { get; private set; }

		readonly Dictionary<char, int> _priceLookup = new Dictionary<char, int>
		{
			{'a', 50},
			{'b', 30}
		};

		public Checkout()
		{
			_itemACount = 0;
			_itemBCount = 0;
		}

		public void ScanBasket(string basket)
		{
			var items = basket.ToCharArray();

			foreach (var item in items)
				AddItemPrice(item);
			SubtractDiscounts();
		}

		private void AddItemPrice(char item)
		{
			Total += _priceLookup[item];
			switch (item)
			{
				case 'a':
					++_itemACount;
					break;
				case 'b':
					++_itemBCount;
					break;
			}
		}

		private void SubtractDiscounts()
		{
			var aDiscount = _itemACount / 3 * 20;
			var bDiscount = _itemBCount / 2 * 15;
			Total -= aDiscount;
			Total -= bDiscount;
		}
	}
}

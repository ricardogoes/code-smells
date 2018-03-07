using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Telerik.JustMock;

namespace CodeSmells.UnitTests.Fragile
{
    [TestFixture]
    public class CartCheckoutShould
    {
        private Cart _cart;
        private ICheckInventory _inventoryService;
        private IPaymentProvider _paymentProvider;
        private ISendEmail _sendEmailService;
        private readonly string _testEmailAddress = "customer@example.com";
        private readonly string _testItemSku = "abc123";
        private readonly decimal _testPrice = 1.23m;

        [SetUp]
        public void SetUp()
        {
             _inventoryService = Mock.Create<ICheckInventory>();
             _paymentProvider = Mock.Create<IPaymentProvider>();
             _sendEmailService = Mock.Create<ISendEmail>();
             _cart = new Cart(_inventoryService, _paymentProvider, _sendEmailService);
        }

        [Test]
        public void CheckInventory()
        {
            Mock.Arrange(() => (_inventoryService.RemainingInventory(_testItemSku))).Returns(1).OccursOnce();

            AddTestItemAndCheckoutWithTestEmailAddress();

            Mock.Assert(_inventoryService);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage="Insufficient inventory")]
        public void ThrowInsufficientInventoryException()
        {
            Mock.Arrange(() => (_inventoryService.RemainingInventory(_testItemSku))).Returns(0).OccursOnce();

            AddTestItemAndCheckoutWithTestEmailAddress();

            Assert.Fail("Should have thrown exception.");
            //Mock.Assert(inventoryService);
        }

        [Test]
        public void ChargeCard()
        {
            Mock.Arrange(() => (_inventoryService.RemainingInventory(_testItemSku))).Returns(1).OccursOnce();
            Mock.Arrange(() => _paymentProvider.ChargeCard(_testPrice, _testEmailAddress)).DoNothing().MustBeCalled();

            AddTestItemAndCheckoutWithTestEmailAddress();

            Mock.Assert(_paymentProvider);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "AVS Mismatch")]
        public void ThrowAVSMismatchException()
        {
            Mock.Arrange(() => (_inventoryService.RemainingInventory(_testItemSku))).Returns(1).OccursOnce();
            Mock.Arrange(() => _paymentProvider.ChargeCard(_testPrice, _testEmailAddress)).Throws(new ApplicationException("AVS Mismatch")).MustBeCalled();

            AddTestItemAndCheckoutWithTestEmailAddress();

            Assert.Fail("Should have thrown exception.");
            //Mock.Assert(paymentProvider);
        }

        [Test]
        public void SendCheckoutMessageOnSuccessfulCheckout()
        {
            Mock.Arrange(() => (_inventoryService.RemainingInventory(_testItemSku))).Returns(1).OccursOnce();
            Mock.Arrange(() => _paymentProvider.ChargeCard(_testPrice, _testEmailAddress)).DoNothing().MustBeCalled();
            Mock.Arrange(() => _sendEmailService.SendCheckoutMessage(_testEmailAddress, _cart)).MustBeCalled();

            AddTestItemAndCheckoutWithTestEmailAddress();

            Mock.Assert(_sendEmailService);

        }

        private void AddTestItemAndCheckoutWithTestEmailAddress()
        {
            _cart.AddItem(_testItemSku);
            _cart.Checkout(_testEmailAddress);
        }
    }

    public interface ISendEmail
    {
        void SendCheckoutMessage(string email, Cart cart);
    }

    public interface ICheckInventory
    {
        int RemainingInventory(string productSKU);
    }

    public interface IPaymentProvider
    {
        void ChargeCard(decimal amount, string email);
    }

    public class Cart
    {
        private readonly ICheckInventory _inventoryService;
        private readonly IPaymentProvider _paymentProvider;
        private List<string> _items = new List<string>();
        private readonly ISendEmail _sendEmailService;

        public Cart(ICheckInventory inventoryService,
            IPaymentProvider paymentProvider,
            ISendEmail sendEmailService)
        {
            this._sendEmailService = sendEmailService;
            this._paymentProvider = paymentProvider;
            this._inventoryService = inventoryService;
        }

        public void Checkout(string email)
        {
            // check inventory
            foreach (string sku in _items)
            {
                int remainingItems = _inventoryService.RemainingInventory(sku);
                if (remainingItems <= 0)
                {
                    throw new ApplicationException("Insufficient inventory");
                }
            }
            _paymentProvider.ChargeCard(1.23m, email);
            _sendEmailService.SendCheckoutMessage(email, this);
        }

        public void AddItem(string sku)
        {
            _items.Add(sku);
        }
    }

}

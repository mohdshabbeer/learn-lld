using System;
using System.Collections.Generic;

namespace InventoryManagementSystem
{
    public enum OrderStatus
    {
        Created,
        Cancelled,
        Returned
    }

    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
    }

    public class InventoryItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; }

        public Dictionary<int, InventoryItem> Inventory
            = new Dictionary<int, InventoryItem>();
    }

    public class Order
    {
        public int OrderId { get; set; }

        public int SupplierId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public OrderStatus Status { get; set; }
    }

    public class InventoryService
    {
        private readonly Dictionary<int, Supplier> suppliers
            = new Dictionary<int, Supplier>();

        private readonly Dictionary<int, Order> orders
            = new Dictionary<int, Order>();

        public void AddSupplier(Supplier supplier)
        {
            suppliers[supplier.SupplierId] = supplier;
        }

        public bool CanFulfillOrder(
            int supplierId,
            int productId,
            int quantity)
        {
            if (!suppliers.ContainsKey(supplierId))
                return false;

            var supplier = suppliers[supplierId];

            if (!supplier.Inventory.ContainsKey(productId))
                return false;

            return supplier.Inventory[productId].Quantity >= quantity;
        }

        public Order PlaceOrder(
            int orderId,
            int supplierId,
            int productId,
            int quantity)
        {
            if (!CanFulfillOrder(
                supplierId,
                productId,
                quantity))
            {
                throw new Exception(
                    "Insufficient Inventory");
            }

            var supplier = suppliers[supplierId];

            supplier.Inventory[productId].Quantity -= quantity;

            var order = new Order
            {
                OrderId = orderId,
                SupplierId = supplierId,
                ProductId = productId,
                Quantity = quantity,
                Status = OrderStatus.Created
            };

            orders[orderId] = order;

            Console.WriteLine(
                $"Order {orderId} placed successfully.");

            return order;
        }

        public void CancelOrder(int orderId)
        {
            if (!orders.ContainsKey(orderId))
                return;

            var order = orders[orderId];

            if (order.Status != OrderStatus.Created)
                return;

            var supplier = suppliers[order.SupplierId];

            supplier.Inventory[order.ProductId].Quantity
                += order.Quantity;

            order.Status = OrderStatus.Cancelled;

            Console.WriteLine(
                $"Order {orderId} cancelled.");
        }

        public void ReturnOrder(int orderId)
        {
            if (!orders.ContainsKey(orderId))
                return;

            var order = orders[orderId];

            if (order.Status != OrderStatus.Created)
                return;

            var supplier = suppliers[order.SupplierId];

            supplier.Inventory[order.ProductId].Quantity
                += order.Quantity;

            order.Status = OrderStatus.Returned;

            Console.WriteLine(
                $"Order {orderId} returned.");
        }

        public void PrintInventory(int supplierId)
        {
            var supplier = suppliers[supplierId];

            Console.WriteLine();
            Console.WriteLine(
                $"Inventory for Supplier: {supplier.Name}");

            foreach (var item in supplier.Inventory.Values)
            {
                Console.WriteLine(
                    $"{item.Product.Name} => {item.Quantity}");
            }

            Console.WriteLine();
        }

         public void Test()
            {
                var inventoryService = new InventoryService();

                //----------------------------------
                // Products
                //----------------------------------

                var laptop = new Product
                {
                    ProductId = 1,
                    Name = "Laptop"
                };

                var mouse = new Product
                {
                    ProductId = 2,
                    Name = "Mouse"
                };

                //----------------------------------
                // Supplier
                //----------------------------------

                var supplier = new Supplier
                {
                    SupplierId = 101,
                    Name = "Dell Supplier"
                };

                supplier.Inventory.Add(
                    laptop.ProductId,
                    new InventoryItem
                    {
                        Product = laptop,
                        Quantity = 10
                    });

                supplier.Inventory.Add(
                    mouse.ProductId,
                    new InventoryItem
                    {
                        Product = mouse,
                        Quantity = 50
                    });

                inventoryService.AddSupplier(supplier);

                //----------------------------------
                // Initial Inventory
                //----------------------------------

                inventoryService.PrintInventory(101);

                //----------------------------------
                // Check Inventory
                //----------------------------------

                bool canFulfill =
                    inventoryService.CanFulfillOrder(
                        101,
                        1,
                        5);

                Console.WriteLine(
                    $"Can Fulfill Laptop Qty 5 ? {canFulfill}");

                //----------------------------------
                // Place Order
                //----------------------------------

                inventoryService.PlaceOrder(
                    orderId: 1001,
                    supplierId: 101,
                    productId: 1,
                    quantity: 5);

                inventoryService.PrintInventory(101);

                //----------------------------------
                // Cancel Order
                //----------------------------------

                inventoryService.CancelOrder(1001);

                inventoryService.PrintInventory(101);

                //----------------------------------
                // Place Another Order
                //----------------------------------

                inventoryService.PlaceOrder(
                    orderId: 1002,
                    supplierId: 101,
                    productId: 2,
                    quantity: 10);

                inventoryService.PrintInventory(101);

                //----------------------------------
                // Return Order
                //----------------------------------

                inventoryService.ReturnOrder(1002);

                inventoryService.PrintInventory(101);

                Console.ReadLine();
            }
        
    }
}
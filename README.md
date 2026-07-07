# Riverdale Restaurant Console System

Simple OOP Project practising proper SOLID principles and applying them to work.
This code is fully written by hand without the use of AI.

## About

Console-based restaurant system built in C# as an OOP practice project. Staff log in as Manager, Chef, or Server and each get their own set of actions. Everything runs in-memory, no database.

## Classes

- `RestaurantBranch` - holds the shared menu, staff, and orders
- `Staff` (abstract) - base class for Manager, Chef, Server
- `IOrderCancel` - implemented by Manager and Server only (Chef can't cancel orders)
- `Menu` / `Item` - the food catalog, searchable by category
- `Order` / `OrderLine` - customer orders, with a status that moves Pending -> Preparing -> Ready -> Served (or Cancelled)

## Notes

- Manager can cancel Pending or Preparing orders, Server can only cancel Pending ones, Chef can't cancel at all
- Cancelling an order restores stock
- Manager can view total sales from Served orders
- Only one active order allowed per table at a time

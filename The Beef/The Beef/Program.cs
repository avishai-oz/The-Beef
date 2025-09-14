using The_Beef.Application.Services;
using The_Beef.Application.Ports;
using The_Beef.Application.DOTs;
using The_Beef.Domain.Entities;
using The_Beef.Infrastructure.Stubs;

// ----- חיווט -----
IMenuRepository menuRepo     = new StubMenuRepository();
ITableRepository tableRepo   = new StubTableRepository();
IUserRepository userRepo     = new StubUserRepository();
IOrderRepository orderRepo   = new StubOrderRepository();
IReceiptRenderer renderer    = new ConsoleReceiptRenderer();

var seating   = new SeatingService(tableRepo);
var selection = new DishSelectionService(menuRepo);
var orders    = new OrderService();
var wallet    = new WalletService(userRepo);
var receipt   = new ReceiptService(menuRepo, renderer);

// ----- נתוני בדיקה -----
var user   = await userRepo.Get("U1") ?? throw new Exception("User not found");
var table  = await seating.AssignTable(user, partySize: 2);
var picks  = await selection.ByNames(new[] { ("Burger", 2), ("Fries", 1) });

// ----- יצירת הזמנה + תמחור -----
var order = orders.Create(user, table, picks);

// ----- חיוב ארנק -----
var walletBefore = user.Wallet;
await wallet.Debit(user, order.FinalAmount);
var walletAfter = user.Wallet;

// ----- קבלה -----
var dto = await receipt.GenerateAsync(order, walletBefore, walletAfter);
receipt.Show(dto);
using The_Beef.Application.Services;
using The_Beef.Application.Ports;
using The_Beef.Application.DOTs;
using The_Beef.Domain.Entities;
using The_Beef.Infrastructure.Json;
using The_Beef.Infrastructure.Stubs;
using The_Beef.Infrastruure.Json;

// ----- חיווט -----
IMenuRepository menuRepo     = new JsonMenuRepository("C:\\Users\\B\\GitHub\\The-Beef\\The Beef\\The Beef\\Data");
ITableRepository tableRepo   = new JsonTableRepository("C:\\Users\\B\\GitHub\\The-Beef\\The Beef\\The Beef\\Data");
IUserRepository userRepo = new JsonUserRepository("C:\\Users\\B\\GitHub\\The-Beef\\The Beef\\The Beef\\Data");
IOrderRepository orderRepo   = new JsonOrderRepository("C:\\Users\\B\\GitHub\\The-Beef\\The Beef\\The Beef\\Data");
IReceiptRenderer renderer    = new ConsoleReceiptRenderer();

var seating   = new SeatingService(tableRepo);
var selection = new DishSelectionService(menuRepo);
var orders    = new OrderService();
var wallet    = new WalletService(userRepo);
var receipt   = new ReceiptService(menuRepo, renderer);

// ----- נתוני בדיקה -----
var user   = await userRepo.Get("U1") ?? throw new Exception("User not found");
var table  = await seating.AssignTable(user, partySize: 2);
var picks  = await selection.ByNames(new[] { ("Burger", 1), ("Fries", 2) });

// ----- יצירת הזמנה + תמחור -----
var order = orders.Create(user, table, picks);

// ----- חיוב ארנק -----
var walletBefore = user.Wallet;
await wallet.Debit(user, order.FinalAmount);
var walletAfter = user.Wallet;

// ----- קבלה -----
var dto = await receipt.GenerateAsync(order, walletBefore, walletAfter);
receipt.Show(dto);
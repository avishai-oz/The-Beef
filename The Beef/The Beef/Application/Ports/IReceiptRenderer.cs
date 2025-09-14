using The_Beef.Application.DOTs;

namespace The_Beef.Application.Ports;

public interface IReceiptRenderer
{
    void Render(ReceiptDto dot);
}
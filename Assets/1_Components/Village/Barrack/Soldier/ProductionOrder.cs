using Firebase.Firestore;

[FirestoreData]
public class ProductionOrder
{
    [FirestoreProperty]
    public string orderId { get; set; }

    [FirestoreProperty]
    public string soldierName { get; set; }

    [FirestoreProperty]
    public double startTimestamp { get; set; }

    [FirestoreProperty]
    public float productionTime { get; set; }

    public ProductionOrder() { }

    public ProductionOrder(string orderId, string soldierName, double startTimestamp, float productionTime)
    {
        this.orderId = orderId;
        this.soldierName = soldierName;
        this.startTimestamp = startTimestamp;
        this.productionTime = productionTime;
    }
}

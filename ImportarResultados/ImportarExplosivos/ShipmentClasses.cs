
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

public partial class Shipment
{

    private string messageIDField;

    private System.DateTime messageTimeField;

    private string messageTypeField;

    private ulong shipmentNumberField;

    private string deliveryNoteNumberField;

    private string purchaseOrderNumberField;

    private System.DateTime expectedDeliveryDateField;

    private ShipmentSender senderField;

    private ShipmentReceiver receiverField;

    private ShipmentSummaryItem[] summaryItemsField;

    private ShipmentUnit[] unitsField;

    private string fileTypeField;

    private string fileCreatorField;

    private decimal fileVersionField;

    private string hashField;

    /// <comentarios/>
    public string MessageID
    {
        get
        {
            return this.messageIDField;
        }
        set
        {
            this.messageIDField = value;
        }
    }

    /// <comentarios/>
    public System.DateTime MessageTime
    {
        get
        {
            return this.messageTimeField;
        }
        set
        {
            this.messageTimeField = value;
        }
    }

    /// <comentarios/>
    public string MessageType
    {
        get
        {
            return this.messageTypeField;
        }
        set
        {
            this.messageTypeField = value;
        }
    }

    /// <comentarios/>
    public ulong ShipmentNumber
    {
        get
        {
            return this.shipmentNumberField;
        }
        set
        {
            this.shipmentNumberField = value;
        }
    }

    /// <comentarios/>
    public string DeliveryNoteNumber
    {
        get
        {
            return this.deliveryNoteNumberField;
        }
        set
        {
            this.deliveryNoteNumberField = value;
        }
    }

    /// <comentarios/>
    public string PurchaseOrderNumber
    {
        get
        {
            return this.purchaseOrderNumberField;
        }
        set
        {
            this.purchaseOrderNumberField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime ExpectedDeliveryDate
    {
        get
        {
            return this.expectedDeliveryDateField;
        }
        set
        {
            this.expectedDeliveryDateField = value;
        }
    }

    /// <comentarios/>
    public ShipmentSender Sender
    {
        get
        {
            return this.senderField;
        }
        set
        {
            this.senderField = value;
        }
    }

    /// <comentarios/>
    public ShipmentReceiver Receiver
    {
        get
        {
            return this.receiverField;
        }
        set
        {
            this.receiverField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlArrayItemAttribute("SummaryItem", IsNullable = false)]
    public ShipmentSummaryItem[] SummaryItems
    {
        get
        {
            return this.summaryItemsField;
        }
        set
        {
            this.summaryItemsField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Unit", IsNullable = false)]
    public ShipmentUnit[] Units
    {
        get
        {
            return this.unitsField;
        }
        set
        {
            this.unitsField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string FileType
    {
        get
        {
            return this.fileTypeField;
        }
        set
        {
            this.fileTypeField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string FileCreator
    {
        get
        {
            return this.fileCreatorField;
        }
        set
        {
            this.fileCreatorField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal FileVersion
    {
        get
        {
            return this.fileVersionField;
        }
        set
        {
            this.fileVersionField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Hash
    {
        get
        {
            return this.hashField;
        }
        set
        {
            this.hashField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentSender
{

    private string codeField;

    private string nameField;

    private string addressField;

    private ushort zipcodeField;

    private string cityField;

    private string countryField;

    /// <comentarios/>
    public string Code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <comentarios/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <comentarios/>
    public string Address
    {
        get
        {
            return this.addressField;
        }
        set
        {
            this.addressField = value;
        }
    }

    /// <comentarios/>
    public ushort Zipcode
    {
        get
        {
            return this.zipcodeField;
        }
        set
        {
            this.zipcodeField = value;
        }
    }

    /// <comentarios/>
    public string City
    {
        get
        {
            return this.cityField;
        }
        set
        {
            this.cityField = value;
        }
    }

    /// <comentarios/>
    public string Country
    {
        get
        {
            return this.countryField;
        }
        set
        {
            this.countryField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentReceiver
{

    private ushort codeField;

    private string nameField;

    private string addressField;

    private ushort zipcodeField;

    private string cityField;

    private string countryField;

    /// <comentarios/>
    public ushort Code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <comentarios/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <comentarios/>
    public string Address
    {
        get
        {
            return this.addressField;
        }
        set
        {
            this.addressField = value;
        }
    }

    /// <comentarios/>
    public ushort Zipcode
    {
        get
        {
            return this.zipcodeField;
        }
        set
        {
            this.zipcodeField = value;
        }
    }

    /// <comentarios/>
    public string City
    {
        get
        {
            return this.cityField;
        }
        set
        {
            this.cityField = value;
        }
    }

    /// <comentarios/>
    public string Country
    {
        get
        {
            return this.countryField;
        }
        set
        {
            this.countryField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentSummaryItem
{

    private string producerProductCodeField;

    private string producerProductNameField;

    private byte packagingLevelField;

    private System.DateTime productionDateField;

    private string sIDField;

    private string pSNField;

    /// <comentarios/>
    public string ProducerProductCode
    {
        get
        {
            return this.producerProductCodeField;
        }
        set
        {
            this.producerProductCodeField = value;
        }
    }

    /// <comentarios/>
    public string ProducerProductName
    {
        get
        {
            return this.producerProductNameField;
        }
        set
        {
            this.producerProductNameField = value;
        }
    }

    /// <comentarios/>
    public byte PackagingLevel
    {
        get
        {
            return this.packagingLevelField;
        }
        set
        {
            this.packagingLevelField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime ProductionDate
    {
        get
        {
            return this.productionDateField;
        }
        set
        {
            this.productionDateField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string SID
    {
        get
        {
            return this.sIDField;
        }
        set
        {
            this.sIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PSN
    {
        get
        {
            return this.pSNField;
        }
        set
        {
            this.pSNField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentUnit
{

    private byte itemQuantityField;

    private byte countOfTradeUnitsField;

    private byte packagingLevelField;

    private ShipmentUnitUnit[] unitsField;

    private ShipmentUnitItem[] itemsField;

    private ulong uIDField;

    private string pSNField;

    private string sIDField;

    /// <comentarios/>
    public byte ItemQuantity
    {
        get
        {
            return this.itemQuantityField;
        }
        set
        {
            this.itemQuantityField = value;
        }
    }

    /// <comentarios/>
    public byte CountOfTradeUnits
    {
        get
        {
            return this.countOfTradeUnitsField;
        }
        set
        {
            this.countOfTradeUnitsField = value;
        }
    }

    /// <comentarios/>
    public byte PackagingLevel
    {
        get
        {
            return this.packagingLevelField;
        }
        set
        {
            this.packagingLevelField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Unit", IsNullable = false)]
    public ShipmentUnitUnit[] Units
    {
        get
        {
            return this.unitsField;
        }
        set
        {
            this.unitsField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
    public ShipmentUnitItem[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong UID
    {
        get
        {
            return this.uIDField;
        }
        set
        {
            this.uIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PSN
    {
        get
        {
            return this.pSNField;
        }
        set
        {
            this.pSNField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string SID
    {
        get
        {
            return this.sIDField;
        }
        set
        {
            this.sIDField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentUnitUnit
{

    private byte itemQuantityField;

    private byte countOfTradeUnitsField;

    private byte packagingLevelField;

    private ShipmentUnitUnitItem[] itemsField;

    private ulong uIDField;

    private string pSNField;

    private string sIDField;

    /// <comentarios/>
    public byte ItemQuantity
    {
        get
        {
            return this.itemQuantityField;
        }
        set
        {
            this.itemQuantityField = value;
        }
    }

    /// <comentarios/>
    public byte CountOfTradeUnits
    {
        get
        {
            return this.countOfTradeUnitsField;
        }
        set
        {
            this.countOfTradeUnitsField = value;
        }
    }

    /// <comentarios/>
    public byte PackagingLevel
    {
        get
        {
            return this.packagingLevelField;
        }
        set
        {
            this.packagingLevelField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
    public ShipmentUnitUnitItem[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong UID
    {
        get
        {
            return this.uIDField;
        }
        set
        {
            this.uIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PSN
    {
        get
        {
            return this.pSNField;
        }
        set
        {
            this.pSNField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string SID
    {
        get
        {
            return this.sIDField;
        }
        set
        {
            this.sIDField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentUnitUnitItem
{

    private string sIDField;

    private ulong uIDField;

    private string pSNField;

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string SID
    {
        get
        {
            return this.sIDField;
        }
        set
        {
            this.sIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong UID
    {
        get
        {
            return this.uIDField;
        }
        set
        {
            this.uIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PSN
    {
        get
        {
            return this.pSNField;
        }
        set
        {
            this.pSNField = value;
        }
    }
}

/// <comentarios/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ShipmentUnitItem
{

    private string sIDField;

    private ulong uIDField;

    private string pSNField;

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string SID
    {
        get
        {
            return this.sIDField;
        }
        set
        {
            this.sIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong UID
    {
        get
        {
            return this.uIDField;
        }
        set
        {
            this.uIDField = value;
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PSN
    {
        get
        {
            return this.pSNField;
        }
        set
        {
            this.pSNField = value;
        }
    }
}

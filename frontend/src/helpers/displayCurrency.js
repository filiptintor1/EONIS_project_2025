const displayCurrencyRSD = (num) => {
    const formater = new Intl.NumberFormat("sr-RS", {
        style: "currency",
        currency: "RSD",
        minimumFractionDigits: 2,
    });

    return formater.format(num);
}

export default displayCurrencyRSD;

function ValidateForm() {
    let forma = document.querySelector("#f1");
    let isValid = true;

    let name = forma["Name"].value;
    let nameSpan = document.querySelector["#nameSpan"];

    let rate = forma["Rate"].value;
    let rateSpan = document.querySelector["#rateSpan"];

    let capacity = forma["EventCapacity"].value;
    let capacitySpan = document.querySelector["#capacitySpan"];

    let date = forma["DateF"].value;
    let dateSpan = document.querySelector["#dateSpan"];

    let price = forma["Price"].value;
    let priceSpan = document.querySelector["#priceSpan"];



    if (!name) {
        nameSpan.innerHTML = "This field is required!";
        isValid = false;
    }
    if (name.length < 3 || name.length > 50) {
        nameSpan.innerHTML = "This field must have a length between 3 and 50 characters!";
        isValid = false;
    } else {
        nameSpan = "";
    }


    if (!rate) {
        rateSpan.innerHTML = "This field is required!";
        isValid = false;
    }
    else if (rate < 1) {
        rateSpan.innerHTML = "This field must have a value between 1 and 5!";
        isValid = false;
    }
    else {
        rateSpan.innerHTML = "";
    }

    if (!capacity) {
        capacitySpan.innerHTML = "This field is required!";
        isValid = false;
    }
    else if (capacity < 1 || capacity > 300000) {
        capacitySpan.innerHTML = "This field must have a value between 1 and 300000!";
        isValid = false;
    }
    else {
        rateSpan.innerHTML = "";
    }
    if (!price) {
       priceSpan.innerHTML = "This field is required!";
        isValid = false;
    }
    else if (price < 500 || price > 40000) {
        priceSpan.innerHTML = "This field must have a value between 500 and 40000!";
        isValid = false;
    }
    else {
        priceSpan.innerHTML = "";
    }
    return isValid;

    //date
    //var a = Date.UTC;
    //if (date < a) {
    //    dateSpan.innerHTML = "You can only select the future!";
    //    isValid = false;
    //} else {
    //   dateSpan = "";
    //}

   
    

}
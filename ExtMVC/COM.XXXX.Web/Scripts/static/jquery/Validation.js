function isTelephone(tel) {
    var reg = /^0?1[3|4|5|8][0-9]\d{8}$/;
    if (reg.test(tel)) {
        return true;
    } else {
        return false;
    };
}
function isEmail(email) {
    var reMail = /^(?:\w+\.?)*\w+@(?:\w+\.?)*\w+$/;
    return reMail.test(email);
}

function isRequire(value) {
    return value ? true : false;
}

function isChina(val) {

    reg = /^[\u4E00-\u9FA5]{2,4}$/;
    if (!reg.test(val)) {

        return false;

    } else {

        return true;
    }

}
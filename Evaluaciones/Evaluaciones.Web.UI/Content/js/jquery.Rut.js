﻿(function (e) {
    jQuery.fn.Rut = function (t) {
        var n = {
            digito_verificador: null,
            on_error: function () { },
            on_success: function () { },
            validation: true,
            format: true,
            format_on: "change"
        };
        var r = e.extend(n, t);
        return this.each(function () {
            if (n.format) {
                jQuery(this).bind(n.format_on, function () {
                    jQuery(this).val(jQuery.Rut.formatear(jQuery(this).val(), n.digito_verificador == null))
                })
            }
            if (n.validation) {
                if (n.digito_verificador == null) {
                    jQuery(this).bind("blur", function () {
                        var e = jQuery(this).val();
                        if (jQuery(this).val() != "" && !jQuery.Rut.validar(e)) {
                            n.on_error()
                        } else if (jQuery(this).val() != "") {
                            n.on_success()
                        }
                    })
                } else {
                    var e = jQuery(this).attr("id");
                    jQuery(n.digito_verificador).bind("blur", function () {
                        var t = jQuery("#" + e).val() + "-" + jQuery(this).val();
                        if (jQuery(this).val() != "" && !jQuery.Rut.validar(t)) {
                            n.on_error()
                        } else if (jQuery(this).val() != "") {
                            n.on_success()
                        }
                    })
                }
            }
        })
    }
})(jQuery);
jQuery.Rut = {
    formatear: function (e, t) {
        var n = new String(e);
        var r = "";
        n = jQuery.Rut.quitarFormato(n);
        if (t) {
            var i = n.charAt(n.length - 1);
            n = n.substring(0, n.length - 1)
        }
        while (n.length > 3) {
            r = "." + n.substr(n.length - 3) + r;
            n = n.substring(0, n.length - 3)
        }
        r = n + r;
        if (r != "" && t) {
            r += "-" + i
        } else if (t) {
            r += i
        }
        return r
    },
    quitarFormato: function (e) {
        var t = new String(e);
        while (t.indexOf(".") != -1) {
            t = t.replace(".", "")
        }
        while (t.indexOf("-") != -1) {
            t = t.replace("-", "")
        }
        return t
    },
    digitoValido: function (e) {
        if (e != "0" && e != "1" && e != "2" && e != "3" && e != "4" && e != "5" && e != "6" && e != "7" && e != "8" && e != "9" && e != "k" && e != "K") {
            return false
        }
        return true
    },
    digitoCorrecto: function (e) {
        largo = e.length;
        if (largo < 2) {
            return false
        }
        if (largo > 2) {
            rut = e.substring(0, largo - 1)
        } else {
            rut = e.charAt(0)
        }
        dv = e.charAt(largo - 1);
        jQuery.Rut.digitoValido(dv);
        if (rut == null || dv == null) {
            return 0
        }
        dvr = jQuery.Rut.getDigito(rut);
        if (dvr != dv.toLowerCase()) {
            return false
        }
        return true
    },
    getDigito: function (e) {
        var t = "0";
        suma = 0;
        mul = 2;
        for (i = e.length - 1; i >= 0; i--) {
            suma = suma + e.charAt(i) * mul;
            if (mul == 7) {
                mul = 2
            } else {
                mul++
            }
        }
        res = suma % 11;
        if (res == 1) {
            return "k"
        } else if (res == 0) {
            return "0"
        } else {
            return 11 - res
        }
    },
    validar: function (e) {
        e = jQuery.Rut.quitarFormato(e);
        largo = e.length;
        if (largo < 2) {
            return false
        }
        for (i = 0; i < largo; i++) {
            if (!jQuery.Rut.digitoValido(e.charAt(i))) {
                return false
            }
        }
        var t = "";
        for (i = largo - 1, j = 0; i >= 0; i--, j++) {
            t = t + e.charAt(i)
        }
        var n = "";
        n = n + t.charAt(0);
        n = n + "-";
        cnt = 0;
        for (i = 1, j = 2; i < largo; i++, j++) {
            if (cnt == 3) {
                n = n + ".";
                j++;
                n = n + t.charAt(i);
                cnt = 1
            } else {
                n = n + t.charAt(i);
                cnt++
            }
        }
        t = "";
        for (i = n.length - 1, j = 0; i >= 0; i--, j++) {
            t = t + n.charAt(i)
        }
        if (jQuery.Rut.digitoCorrecto(e)) {
            return true
        }
        return false
    }
}
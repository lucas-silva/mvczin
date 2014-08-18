(function ($) {
    $.validator.addMethod('cnpj', function (value, element) {
        if (value && value.trim() == '') {
            return true;
        }

        var cnpj = threatCnpj(value);

        var isInvalid = isInvalidLength(cnpj) || isNotNumbersOnly(cnpj) || isInvalidCnpj(cnpj);

        var isValid = !isInvalid;

        return isValid;
    }, '');

    $.validator.unobtrusive.adapters.add('cnpj', function (options) {
        options.rules["cnpj"] = '#' + options.params;
        options.messages['cnpj'] = options.message;
    });

    function threatCnpj(cnpj) {
        return cnpj.trim().replace(/\./gi, '').replace(/-/gi, '').replace(/\//gi, '');
    }

    function getFirstDigit(cnpj) {
        var cnpjToWork = cnpj.substring(0, 12);

        var multipliers = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        var sum = 0;

        for (var i = 0; i < multipliers.length; i++) {
            sum += parseInt(cnpjToWork[i].toString()) * multipliers[i];
        }

        var rest = sum % 11;

        var firstDigit = rest < 2 ? 0 : 11 - rest;

        return firstDigit;
    }

    function getSecondDigit(cnpj, firstDigit) {
        var cnpjToWork = cnpj.substring(0, 12) + firstDigit;

        var multipliers = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        var sum = 0;

        for (var i = 0; i < multipliers.length; i++) {
            sum += parseInt(cnpjToWork[i].toString()) * multipliers[i];
        }

        var rest = sum % 11;

        var secondDigit = rest < 2 ? 0 : 11 - rest;

        return secondDigit;
    }

    function isInvalidLength(cnpj) {
        return cnpj.length != 14;
    }

    function isNotNumbersOnly(cnpj) {
        return !/^\d+$/.test(cnpj);
    }

    function isInvalidCnpj(cnpj) {
        var firstDigit = getFirstDigit(cnpj);

        var secondDigit = getSecondDigit(cnpj, firstDigit);

        var expectedSufix = firstDigit.toString() + secondDigit.toString();

        var endsWithPattern = new RegExp(expectedSufix + '$')

        var isInvalid = !endsWithPattern.test(cnpj);

        return isInvalid;
    }
})(jQuery);
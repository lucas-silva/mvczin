(function ($) {
    $.validator.addMethod('cnpj', function (value, element) {
        var cnpj = threatCnpj(value);

        if (cnpj == '') {
            return true;
        }

        var isInvalid = isInvalidLength(cnpj) || isNotNumbersOnly(cnpj) || isInvalidCnpj(cnpj) || isInvalidSequence(cnpj);

        var isValid = !isInvalid;

        return isValid;
    }, '');

    $.validator.unobtrusive.adapters.add('cnpj', function (options) {
        options.rules["cnpj"] = '#' + options.params;
        options.messages['cnpj'] = options.message;
    });

    function threatCnpj(cnpj) {
        if (cnpj) {
            return cnpj.trim().replace(/\D/g, '');
        }
        else {
            return '';
        }
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
	
	function isInvalidSequence(cnpj) {
		if (cnpj == "00000000000000" ||
			cnpj == "11111111111111" ||
			cnpj == "22222222222222" ||
			cnpj == "33333333333333" ||
			cnpj == "44444444444444" ||
			cnpj == "55555555555555" ||
			cnpj == "66666666666666" ||
			cnpj == "77777777777777" ||
			cnpj == "88888888888888" ||
			cnpj == "99999999999999")
		{
		    return true;
		}

		return false;
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

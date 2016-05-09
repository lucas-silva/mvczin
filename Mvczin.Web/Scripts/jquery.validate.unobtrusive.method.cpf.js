(function ($) {
    $.validator.addMethod('cpf', function (value, element) {
        var cpf = threatCpf(value);

        if (cpf == '') {
            return true;
        }

        var isInvalid = isInvalidLength(cpf) || isNotNumbersOnly(cpf) || isInvalidCpf(cpf) || isInvalidSequence(cpf);

        var isValid = !isInvalid;

        return isValid;
    }, '');

    $.validator.unobtrusive.adapters.add('cpf', function (options) {
        options.rules["cpf"] = '#' + options.params;
        options.messages['cpf'] = options.message;
    });

    function threatCpf(cpf) {
        if (cpf) {
            return cpf.trim().replace(/\D/g, '');
        }
        else {
            return '';
        }
    }

    function getFirstDigit(cpf) {
        var multipliers = [10, 9, 8, 7, 6, 5, 4, 3, 2];

        var cpfToWork = cpf.substring(0, 9);

        var sum = 0;

        for (var i = 0; i < multipliers.length; i++) {
            sum += parseInt(cpfToWork[i].toString()) * multipliers[i];
        }

        var rest = sum % 11;

        var firstDigit = rest < 2 ? 0 : 11 - rest;

        return firstDigit;
    }

    function getSecondDigit(cpf, firstDigit) {
        var multipliers = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var cpfToWork = cpf.substring(0, 9) + firstDigit;

        var sum = 0;

        for (var i = 0; i < multipliers.length; i++) {
            sum += parseInt(cpfToWork[i].toString()) * multipliers[i];
        }

        var rest = sum % 11;

        var secondDigit = rest < 2 ? 0 : 11 - rest;

        return secondDigit;
    }

    function isInvalidLength(cnpj) {
        return cnpj.length != 11;
    }
	
	function isInvalidSequence(cpf) {
		if (cpf == "00000000000" ||
			cpf == "11111111111" ||
			cpf == "22222222222" ||
			cpf == "33333333333" ||
			cpf == "44444444444" ||
			cpf == "55555555555" ||
			cpf == "66666666666" ||
			cpf == "77777777777" ||
			cpf == "88888888888" ||
			cpf == "99999999999")
		{
			return true;
		}

		return false;
	}

    function isNotNumbersOnly(cnpj) {
        return !/^\d+$/.test(cnpj);
    }

    function isInvalidCpf(cpf) {
        var firstDigit = getFirstDigit(cpf);

        var secondDigit = getSecondDigit(cpf, firstDigit);

        var expectedSufix = firstDigit.toString() + secondDigit.toString();

        var endsWithPattern = new RegExp(expectedSufix + '$')

        var isInvalid = !endsWithPattern.test(cpf);

        return isInvalid;
    }
})(jQuery);

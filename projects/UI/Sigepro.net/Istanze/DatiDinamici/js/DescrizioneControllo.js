﻿/*jslint browser: true*/
/*jslint plusplus: true */
/*jslint continue:true */
/*global $, jQuery, alert */
$(function () {
    'use strict';

    $('.helpIcon').on('click', function (e) {
        var ctrlId = this.id,
            targetId = ctrlId.replace('__imgDescrizione', '__descrizione'),
            targetCtrl = $('#' + targetId);


        var altezza = Math.floor(targetCtrl.html().length / 80);

        targetCtrl.dialog({
            modal: true,
            title: 'Informazioni',
            width: 400,
            height: 30 * altezza + 80
        });
    });

    /*
	$('.helpIcon').mouseover(function () {
		var ctrlId = this.id,
            targetId = ctrlId.replace('__imgDescrizione', '__descrizione'),
            pos = $(this).position(),
            width = $(this).width(),
            finalTop = pos.top,
            finalLeft = pos.left + width,
            target = $('#' + targetId);
		
		if ($(window).width() < target.width() + finalLeft) {
			finalLeft = pos.left - target.width() - 5;
        }
		
		
		target.css('top', finalTop + 'px')
			  .css('left', finalLeft + 'px')
			  .fadeIn();
		
		
		
		//mostraDescrizioneCampo(targetId);
	});
    */
    /*
	$('.helpIcon').mouseout(function () {
		var ctrlId = this.id,
            targetId = ctrlId.replace('__imgDescrizione', '__descrizione');

		$('#' + targetId).fadeOut();
	});
    */
});
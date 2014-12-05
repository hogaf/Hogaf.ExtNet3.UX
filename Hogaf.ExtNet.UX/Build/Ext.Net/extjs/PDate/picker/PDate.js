/*

This file is part of Ext JS 4

Copyright (c) 2011 Sencha Inc

Contact:  http://www.sencha.com/contact

GNU General Public License Usage
This file may be used under the terms of the GNU General Public License version 3.0 as published by the Free Software Foundation and appearing in the file LICENSE included in the packaging of this file.  Please review the following information to ensure the GNU General Public License version 3.0 requirements will be met: http://www.gnu.org/copyleft/gpl.html.

If you are unsure which license is appropriate for your use, please contact the sales department at http://www.sencha.com/contact.

*/
/**
 * @class Ext.picker.Date
 * @extends Ext.Component
 * <p>A date picker. This class is used by the {@link Ext.form.field.Date} field to allow browsing and
 * selection of valid dates in a popup next to the field, but may also be used with other components.</p>
 * <p>Typically you will need to implement a handler function to be notified when the user chooses a color from the
 * picker; you can register the handler using the {@link #select} event, or by implementing the {@link #handler}
 * method.</p>
 * <p>By default the user will be allowed to pick any date; this can be changed by using the {@link #minDate},
 * {@link #maxDate}, {@link #disabledDays}, {@link #disabledDatesRE}, and/or {@link #disabledDates} configs.</p>
 * <p>All the string values documented below may be overridden by including an Ext locale file in your page.</p>
 * <p>Example usage:</p>
 * <pre><code>new Ext.panel.Panel({
    title: 'Choose a future date:',
    width: 200,
    bodyPadding: 10,
    renderTo: Ext.getBody(),
    items: [{
        xtype: 'datepicker',
        minDate: new Date(),
        handler: function(picker, date) {
            // do something with the selected date
        }
    }]
});</code></pre>
 * {@img Ext.picker.Date/Ext.picker.Date.png Ext.picker.Date component}
 *
 */
Ext.define('Ext.picker.PDate', {
    extend: 'Ext.picker.Date',
    alias: 'widget.pdatepicker',
    alternateClassName: 'Ext.PDatePicker',
    requires: ['Ext.picker.PMonth'],
    /**
     * @cfg {Number} startDay
     * Day index at which the week should begin, 0-based (defaults to 6, which is Sunday)
     */
    //<locale>
    startDay: 0,
    //</locale>    

    initEvents: function () {
        var me = this,
            pickerField = me.pickerField,
            eDate = Ext.PDate,
            day = eDate.DAY;

        //me.callParent();
        Ext.picker.Date.superclass.initEvents.call(this);

        // If this is not focusable (eg being used as the picker of a DateField)
        // then prevent mousedown from blurring the input field.
        if (!me.focusable) {
            me.el.on({
                mousedown: me.onMouseDown
            });
        }

        me.prevRepeater = new Ext.util.ClickRepeater(me.prevEl, {
            handler: me.showPrevMonth,
            scope: me,
            preventDefault: true,
            stopDefault: true
        });

        me.nextRepeater = new Ext.util.ClickRepeater(me.nextEl, {
            handler: me.showNextMonth,
            scope: me,
            preventDefault: true,
            stopDefault: true
        });

        // Read key events through our pickerField if we are bound to one
        me.keyNav = new Ext.util.KeyNav(pickerField ? pickerField.inputEl : me.eventEl, Ext.apply({
            scope: me,

            // Must capture event so that the Picker sees it before the Field.
            capture: true,

            left: function (e) {
                if (e.ctrlKey) {
                    me.showPrevMonth();
                } else {
                    me.update(eDate.add(me.activeDate, day, (me.inheritedState.rtl ? -1 : 1) * -1));
                }
            },

            right: function (e) {
                if (e.ctrlKey) {
                    me.showNextMonth();
                } else {
                    me.update(eDate.add(me.activeDate, day, (me.inheritedState.rtl ? -1 : 1) * 1));
                }
            },

            up: function (e) {
                if (e.ctrlKey) {
                    me.showNextYear();
                } else {
                    me.update(eDate.add(me.activeDate, day, -7));
                }
            },

            down: function (e) {
                if (e.ctrlKey) {
                    me.showPrevYear();
                } else {
                    me.update(eDate.add(me.activeDate, day, 7));
                }
            },

            pageUp: function (e) {
                if (e.ctrlKey) {
                    me.showPrevYear();
                } else {
                    me.showPrevMonth();
                }
            },

            pageDown: function (e) {
                if (e.ctrlKey) {
                    me.showNextYear();
                } else {
                    me.showNextMonth();
                }
            },

            tab: function (e) {
                me.handleTabClick(e);

                // Allow default behaviour of TAB - it MUST be allowed to navigate.
                return true;
            },

            enter: function (e) {
                me.handleDateClick(e, me.activeCell.firstChild);
            },

            space: function () {
                me.setValue(new Date(me.activeCell.firstChild.dateValue));
                var startValue = me.startValue,
                    value = me.value,
                    pickerValue;

                if (pickerField) {
                    pickerValue = pickerField.getValue();
                    if (pickerValue && startValue && pickerValue.getTime() === value.getTime()) {
                        pickerField.setValue(startValue);
                    } else {
                        pickerField.setValue(value);
                    }
                }
            },

            home: function (e) {
                me.update(eDate.getFirstDateOfMonth(me.activeDate));
            },

            end: function (e) {
                me.update(eDate.getLastDateOfMonth(me.activeDate));
            }
        }, me.keyNavConfig));

        if (me.disabled) {
            me.syncDisabled(true);
        }
        me.update(me.value);
    },

    /**
     * Setup the disabled dates regex based on config options
     * @private
     */
    initDisabledDays: function () {
        var me = this,
            dd = me.disabledDates,
            re = '(?:',
            len,
            d, dLen, dI;

        if (!me.disabledDatesRE && dd) {
            len = dd.length - 1;

            dLen = dd.length;

            for (d = 0; d < dLen; d++) {
                dI = dd[d];

                re += Ext.isDate(dI) ? '^' + Ext.String.escapeRegex(Ext.PDate.dateFormat(dI, me.format)) + '$' : dI;
                if (d !== len) {
                    re += '|';
                }
            }

            me.disabledDatesRE = new RegExp(re + ')');
        }
    },

    /**
     * Create the month picker instance
     * @private
     * @return {Ext.picker.Month} picker
     */
    createMonthPicker: function () {
        var me = this,
            picker = me.monthPicker;

        if (!picker) {
            me.monthPicker = picker = new Ext.picker.PMonth({
                renderTo: me.el,
                floating: true,
                padding: me.padding,
                shadow: false,
                small: me.showToday === false,
                listeners: {
                    scope: me,
                    cancelclick: me.onCancelClick,
                    okclick: me.onOkClick,
                    yeardblclick: me.onOkClick,
                    monthdblclick: me.onOkClick
                }
            });
            if (!me.disableAnim) {
                // hide the element if we're animating to prevent an initial flicker
                picker.el.setStyle('display', 'none');
            }
            picker.hide();
            me.on('beforehide', me.doHideMonthPicker, me);
        }
        return picker;
    },

    /**
     * Respond to an ok click on the month picker
     * @private
     */
    onOkClick: function (picker, value) {
        var me = this,
            month = value[0],
            year = value[1],
            gd = Ext.PDate.PersianToGregorian(year, month, Ext.PDate.getDate(me.getActive())),
            date = new Date(gd[0], month == 0 ? gd[1] - 1 : gd[1], gd[2]);

        //if (Ext.PDate.getMonth(date) !== month) {
        //    // 'fix' the JS rolling date conversion if needed
        //    date = Ext.Date.getLastDateOfMonth(new Date(gd[0], gd[1], 1));
        //}

        me.update(date);
        me.hideMonthPicker();
    },

    /**
     * Show the previous month.
     * @return {Ext.picker.Date} this
     */
    showPrevMonth: function (e) {
        return this.update(Ext.PDate.add(this.activeDate, Ext.Date.MONTH, (this.inheritedState.rtl ? -1 : 1) * -1));
    },

    /**
     * Show the next month.
     * @return {Ext.picker.Date} this
     */
    showNextMonth: function (e) {
        return this.update(Ext.PDate.add(this.activeDate, Ext.Date.MONTH, (this.inheritedState.rtl ? -1 : 1) * 1));
    },

    /**
     * Show the previous year.
     * @return {Ext.picker.Date} this
     */
    showPrevYear: function () {
        this.update(Ext.PDate.add(this.activeDate, Ext.Date.YEAR, -1));
    },

    /**
     * Show the next year.
     * @return {Ext.picker.Date} this
     */
    showNextYear: function () {
        this.update(Ext.PDate.add(this.activeDate, Ext.Date.YEAR, 1));
    },

    /**
    * Update the contents of the picker
    * @private
    * @param {Date} date The new date
    * @param {Boolean} forceRefresh True to force a full refresh
    */
    update: function (date, forceRefresh) {
        var me = this,
            active = me.activeDate;

        if (me.rendered) {
            me.activeDate = date;
            if (!forceRefresh && active && me.el && Ext.PDate.getMonth(active) == Ext.PDate.getMonth(date) && Ext.PDate.getFullYear(active) == Ext.PDate.getFullYear(date)) {
                me.selectedUpdate(date, active);
            } else {
                me.fullUpdate(date, active);
            }
            //me.innerEl.dom.title = Ext.String.format(me.ariaTitle, Ext.PDate.format(me.activeDate, me.ariaTitleDateFormat));
        }
        return me;
    },

    /**
     * Update the contents of the picker for a new month
     * @private
     * @param {Date} date The new date
     */    
    fullUpdate: function(date) {
    var me = this,
        cells = me.cells.elements,
        textNodes = me.textNodes,
        disabledCls = me.disabledCellCls,
        eDate = Ext.PDate,
        i = 0,
        extraDays = 0,
        newDate = +eDate.clearTime(date, true),
        today = +eDate.clearTime(new Date()),
        min = me.minDate ? eDate.clearTime(me.minDate, true) : Number.NEGATIVE_INFINITY,
        max = me.maxDate ? eDate.clearTime(me.maxDate, true) : Number.POSITIVE_INFINITY,
        ddMatch = me.disabledDatesRE,
        ddText = me.disabledDatesText,
        ddays = me.disabledDays ? me.disabledDays.join('') : false,
        ddaysText = me.disabledDaysText,
        format = me.format,
        days = eDate.getDaysInMonth(date),
        firstOfMonth = eDate.getFirstDateOfMonth(date),
        startingPos = firstOfMonth.getDay() - me.startDay,
        previousMonth = eDate.add(date, eDate.MONTH, -1),
        ariaTitleDateFormat = me.ariaTitleDateFormat,
        prevStart, current, disableToday, tempDate, setCellClass, html, cls,
        formatValue, value;

    if (startingPos < 0) {
        startingPos += 7;
    }

    days += startingPos;
    prevStart = eDate.getDaysInMonth(previousMonth) - startingPos;
    //current = new Date(previousMonth.getFullYear(), previousMonth.getMonth(), prevStart, me.initHour);
    current = eDate.clone(previousMonth);
    current = eDate.clearTime(current);
    eDate.setDate(current, prevStart);
    current.setHours(me.initHour);

    if (me.showToday) {
        tempDate = eDate.clearTime(new Date());
        disableToday = (tempDate < min || tempDate > max ||
            (ddMatch && format && ddMatch.test(eDate.dateFormat(tempDate, format))) ||
            (ddays && ddays.indexOf(tempDate.getDay()) != -1));

        if (!me.disabled) {
            me.todayBtn.setDisabled(disableToday);
        }
    }

    setCellClass = function(cellIndex, cls){
        var cell = cells[cellIndex];
            
        value = +eDate.clearTime(current, true);
        cell.setAttribute('aria-label', eDate.format(current, ariaTitleDateFormat));
        // store dateValue number as an expando
        cell.firstChild.dateValue = value;
        if (value == today) {
            cls += ' ' + me.todayCls;
            cell.firstChild.title = me.todayText;
                
            // Extra element for ARIA purposes
            me.todayElSpan = Ext.DomHelper.append(cell.firstChild, {
                tag: 'span',
                cls: Ext.baseCSSPrefix + 'hidden-clip',
                html: me.todayText
            }, true);
        }
        if (value == newDate) {
            me.activeCell = cell;
            me.eventEl.dom.setAttribute('aria-activedescendant', cell.id);
            cell.setAttribute('aria-selected', true);
            cls += ' ' + me.selectedCls;
            me.fireEvent('highlightitem', me, cell);
        } else {
            cell.setAttribute('aria-selected', false);
        }

        if (value < min) {
            cls += ' ' + disabledCls;
            cell.setAttribute('aria-label', me.minText);
        }
        else if (value > max) {
            cls += ' ' + disabledCls;
            cell.setAttribute('aria-label', me.maxText);
        }
        else if (ddays && ddays.indexOf(current.getDay()) !== -1){
            cell.setAttribute('aria-label', ddaysText);
            cls += ' ' + disabledCls;
        }
        else if (ddMatch && format){
            formatValue = eDate.dateFormat(current, format);
            if(ddMatch.test(formatValue)){
                cell.setAttribute('aria-label', ddText.replace('%0', formatValue));
                cls += ' ' + disabledCls;
            }
        }
        cell.className = cls + ' ' + me.cellCls;
    };

    for(; i < me.numDays; ++i) {
        if (i < startingPos) {
            html = (++prevStart);
            cls = me.prevCls;
        } else if (i >= days) {
            html = (++extraDays);
            cls = me.nextCls;
        } else {
            html = i - startingPos + 1;
            cls = me.activeCls;
        }
        textNodes[i].innerHTML = html;
        current.setDate(current.getDate() + 1);
        setCellClass(i, cls);
    }

    me.monthBtn.setText(Ext.PDate.format(date, me.monthYearFormat));
}
},

// After dependencies have loaded:
function () {
    var proto = this.prototype,
        date = Ext.PDate;

    proto.monthNames = date.monthNames;
    proto.dayNames = date.dayNames;
    proto.format = date.defaultFormat;
});



Ext.data.Types.PDATE = {
    convert: function (v) {
        var df = this.dateFormat;
        if (!v) {
            return null;
        }
        if (Ext.isDate(v)) {
            return v;
        }
        if (df) {
            if (df == 'timestamp') {
                return new Date(v * 1000);
            }
            if (df == 'time') {
                return new Date(parseInt(v, 10));
            }
            return Ext.PDate.parse(v, df);
        }

        var parsed = Date.parse(v);
        return parsed ? new Date(parsed) : null;
    },
    sortType: Ext.data.SortTypes.asDate,
    type: 'pdate'
}
Ext.util.Format.pdate = function (v, format) {
    if (!v) {
        return "";
    }
    if (!Ext.isDate(v)) {
        v = new Date(Date.parse(v));
    }
    return Ext.PDate.dateFormat(v, format || Ext.PDate.defaultFormat);
};
Ext.util.Format.pdateRenderer = function (format) {
    return function (v) {
        return UtilFormat.date(v, format);
    }
}




/*

This file is part of Ext JS 4

Copyright (c) 2011 Sencha Inc

Contact:  http://www.sencha.com/contact

GNU General Public License Usage
This file may be used under the terms of the GNU General Public License version 3.0 as published by the Free Software Foundation and appearing in the file LICENSE included in the packaging of this file.  Please review the following information to ensure the GNU General Public License version 3.0 requirements will be met: http://www.gnu.org/copyleft/gpl.html.

If you are unsure which license is appropriate for your use, please contact the sales department at http://www.sencha.com/contact.

*/
/**
 * @class Ext.menu.DatePicker
 * @extends Ext.menu.Menu
 * <p>A menu containing an {@link Ext.picker.Date} Component.</p>
 * <p>Notes:</p><div class="mdetail-params"><ul>
 * <li>Although not listed here, the <b>constructor</b> for this class
 * accepts all of the configuration options of <b>{@link Ext.picker.Date}</b>.</li>
 * <li>If subclassing DateMenu, any configuration options for the DatePicker must be
 * applied to the <tt><b>initialConfig</b></tt> property of the DateMenu.
 * Applying {@link Ext.picker.Date DatePicker} configuration settings to
 * <b><tt>this</tt></b> will <b>not</b> affect the DatePicker's configuration.</li>
 * </ul></div>
 *
 * {@img Ext.menu.DatePicker/Ext.menu.DatePicker.png Ext.menu.DatePicker component}
 *
 * __Example Usage__
 *
 *     var dateMenu = Ext.create('Ext.menu.DatePicker', {
 *         handler: function(dp, date){
 *             Ext.Msg.alert('Date Selected', 'You choose {0}.', Ext.Date.format(date, 'M j, Y'));
 *         }
 *     });
 *  
 *     Ext.create('Ext.menu.Menu', {
 *         width: 100,
 *         height: 90,
 *         floating: false,  // usually you want this set to True (default)
 *         renderTo: Ext.getBody(),  // usually rendered by it's containing component
 *         items: [{
 *             text: 'choose a date',
 *             menu: dateMenu
 *         },{
 *             iconCls: 'add16',
 *             text: 'icon item'
 *         },{
 *             text: 'regular item'
 *         }]
 *     });
 *
 * @author Nicolas Ferrero
 */
Ext.define('Ext.menu.PDatePicker', {
    extend: 'Ext.menu.Menu',

    alias: 'widget.pdatemenu',

    requires: [
       'Ext.picker.PDate'
    ],

    /**
     * @cfg {Boolean} hideOnClick
     * False to continue showing the menu after a date is selected, defaults to true.
     */
    hideOnClick: true,

    /**
     * @cfg {String} pickerId
     * An id to assign to the underlying date picker. Defaults to <tt>null</tt>.
     */
    pickerId: null,

    /**
     * @cfg {Number} maxHeight
     * @private
     */

    /**
     * The {@link Ext.picker.Date} instance for this DateMenu
     * @property picker
     * @type Ext.picker.Date
     */

    initComponent: function () {
        var me = this;

        Ext.apply(me, {
            showSeparator: false,
            plain: true,
            border: false,
            bodyPadding: 0, // remove the body padding from the datepicker menu item so it looks like 3.3
            items: Ext.applyIf({
                cls: Ext.baseCSSPrefix + 'menu-date-item',
                id: me.pickerId,
                xtype: 'pdatepicker'
            }, me.initialConfig)
        });

        me.callParent(arguments);

        me.picker = me.down('pdatepicker');
        /**
         * @event select
         * @inheritdoc Ext.picker.Date#select
         */
        me.relayEvents(me.picker, ['select']);

        if (me.hideOnClick) {
            me.on('select', me.hidePickerOnSelect, me);
        }
    },

    hidePickerOnSelect: function () {
        Ext.menu.Manager.hideAll();
    }
});

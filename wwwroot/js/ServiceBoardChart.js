export function LoadChart(details) {
    var Options = {
        colors: 
            ['#f5c542', '#aaf542', '#4842f5', '#42c8f5'],
        series: [{
            name: 'Stores',
            data: details.stores
        }, {
            name: 'Visits',
            data: details.visits
        }, {
            name: 'Deliveries',
            data: details.deliveries
        }, {
            name: 'Carts',
            data: details.carts
        }],
        chart: {
            type: 'bar',
            height: 350
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
            },
        },
        dataLabels: {
            enabled: false
        },
       
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: details.category,
        },
        fill: {
            opacity: 1
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return val
                }
            }
        },
    };

    var chart = new ApexCharts(document.querySelector("#chart"), Options);
    chart.render();
}
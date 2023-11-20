class ForeignKey {
    constructor(key, relates) {
        this.key = key;
        this.relates = relates;
    }
}
class EntitySet {
    constructor(name) {
        this.name = name;
        this.pk = new Array();
        this.attr = new Array();
    }

    add_pk(pk) {
        this.pk.push(pk);
    }

    add_fk(fk) {
        this.fk = fk;
    }

    add_attr(attr) {
        this.attr.push(attr);
    }

    draw(x, y) {
        let tbl = document.getElementById(this.name);
        tbl.style.top = y + "px";
        tbl.style.left = x + "px";

        let thead = tbl.createTHead();
        let row = thead.insertRow();
        let th = document.createElement("th");
        let name = document.createTextNode(this.name);
        th.appendChild(name);
        row.appendChild(th);

        let td = document.createElement("td");

        let tbody = tbl.createTBody();
        this.pk.forEach((pk) => {
            console.log(pk);
            row = tbody.insertRow();
            td.innerHTML = "<u>" + pk + "</u>";
            td.id = this.name + "-pk";
            row.appendChild(td);
        });

        this.attr.forEach((attr) => {
            row = tbody.insertRow();
            td = document.createElement("td");
            td.innerHTML = attr;
            row.appendChild(td);
        });
    }
}

class Relation {
    constructor(t1, card1, t2, card2, relation) {
        this.t1 = t1;
        this.t2 = t2;
        this.card1 = card1;
        this.card2 = card2;
        this.relation = relation;
    }

    draw() {
        let textDiv = document.getElementById(this.relation);
        textDiv.innerText = this.relation;
        let contDiv = document.getElementById(this.relation + "-container");
        let t1pos = document.getElementById(this.t1).getBoundingClientRect();
        let t2pos = document.getElementById(this.t2).getBoundingClientRect();

        let t1Center = {y: (t1pos.bottom - t1pos.top) / 2 + t1pos.top, x: (t1pos.right - t1pos.left) / 2 + t1pos.left};
        let t2Center = {y: (t2pos.bottom - t2pos.top) / 2 + t2pos.top, x: (t2pos.right - t2pos.left) / 2 + t2pos.left};
        let t1dim = {width: t1pos.right - t1pos.left, height: t1pos.bottom - t1pos.top};
        let t2dim = {width: t2pos.right - t2pos.left, height: t2pos.bottom - t2pos.top};
        //let t2Center = {y: t2pos.bottom - t2pos.top, x: t2pos.right - t2pos.left};

        console.log("t1center is :", t1Center);
        console.log("t2center is :", t2Center);

        let x = ((t2Center.x - t1Center.x) / 2) + t1Center.x - contDiv.clientWidth / 2;
        let y = ((t2Center.y - t1Center.y) / 2) + t1Center.y - contDiv.clientWidth / 2;
        //let y = ((t1pos.top + t2pos.top) / 2) * 1.5 + contDiv.clientHeight / 2;
        contDiv.style.top = y + "px";
        contDiv.style.left = x + "px";

        let contDivPos = contDiv.getBoundingClientRect();
        //console.log(contDiv);
        console.log(contDivPos);
        let contDivCenter = {
            y: (contDivPos.bottom - contDivPos.top) / 2 + contDivPos.top, 
            x: (contDivPos.right - contDivPos.left) / 2 + contDivPos.left
        };
        console.log(contDivCenter);

        let t1_r = document.getElementById(this.t1 + "-" + this.relation);
        let t2_r = document.getElementById(this.t2 + "-" + this.relation);

        let offsetWidth = 0;
        let offsetHeight = 0;

        if (t1Center.x != t2Center.x) {
            offsetWidth = 1;
        }
        if (t1Center.y != t2Center.y) {
            offsetHeight = 1;
        }

        console.log("offset width: %d, offset height %d", offsetWidth, offsetHeight)

        t1_r.setAttribute('x2', t1Center.x + (t1pos.width * offsetWidth/2 + 10 * offsetWidth));
        t1_r.setAttribute('x1', contDivCenter.x  - (contDivPos.width * offsetWidth/2 - 20 * offsetWidth) - 2.4);
        t1_r.setAttribute('y2', t1Center.y + (t1pos.height * offsetHeight/2 + 10 * offsetHeight));
        t1_r.setAttribute('y1', contDivCenter.y - (contDivPos.height * offsetHeight/2 - 10 * offsetHeight) - 2.4);
        console.log(t1_r);

        t2_r.setAttribute('x2', t2Center.x - (t2pos.width * offsetWidth/2 - 10 * offsetWidth));
        t2_r.setAttribute('x1', contDivCenter.x + (contDivPos.width * offsetWidth/2 - 10 * offsetWidth) - 2.4);
        t2_r.setAttribute('y2', t2Center.y - (t2pos.height * offsetHeight/2 - 10 * offsetHeight));
        t2_r.setAttribute('y1', contDivCenter.y + (contDivPos.height * offsetHeight/2 + 10 * offsetHeight) - 2.4);
        console.log(t2_r);

        if (this.card2 == 1) {
            console.log("hello");
            let svg = document.getElementById("svg-container");
            let dblLine = document.createElement("line");
            dblLine.setAttribute('x2', t2Center.x - (t2pos.width * offsetWidth/2 - 20 * offsetHeight));
            dblLine.setAttribute('x1', contDivCenter.x + (contDivPos.width * offsetWidth/2 - 20 * offsetHeight) - 2.4);
            dblLine.setAttribute('y2', t2Center.y - (t2pos.height * offsetHeight/2 - 20 * offsetWidth));
            dblLine.setAttribute('y1', contDivCenter.y + (contDivPos.height * offsetHeight/2 + 20 * offsetWidth) - 2.4);
            dblLine.className = "solid-stroke";
            svg.appendChild(dblLine);
            console.log(svg);
        }
    }
}

function draw(relations){

}
import React from 'react';
import { TileRow } from './tile-row/TileRow';

const data = [
    {
        id: 1,
        title: "Title 1",
    },
    {
        id: 2,
        title: "Title 2",
    },
    {
        id: 3,
        title: "Title 3",
    },
    {
        id: 4,
        title: "Title 4",
    },
    {
        id: 5,
        title: "Title 5",
    },
    {
        id: 6,
        title: "Title 6",
    },
];

const array = [
    "Option 1",
    "Option 2",
    "Option 3",
    "Option 4",
    "Option 5",
    "Option 6",
    "Option 7",
    "Option 8",
];

export const TileGroup = (props) => {

    const renderRows = (data) => {
        let rows = [];
        for (let i = 0; i < data.length; i += 3) {
            rows.push(<TileRow key={i} data={data.slice(i, i + 3)} />);
        }

        return rows;
    }

    return (
        <div className="tile-group">
            {renderRows(data)}
        </div>
    )
}


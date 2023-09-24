import EditIcon from "@/ui/icons/edit";
import CrossIcon from "@/ui/icons/cross";
import {deleteCustomer, updateCustomer} from "@/app/api/customers";
import {Customer} from "@/app/types/entities/Customer";
import EyeIcon from "@/ui/icons/eye";
import toast from "react-hot-toast";
import Link from "next/link";
import {MouseEvent} from "react";

export default function Actions({customer, handleRefresh, loading}: {
    customer: Customer,
    handleRefresh: Function,
    loading: boolean
}) {
    /**
     * Delete project handler
     */
    function handleDelete() {
        if (loading) return;

        if (!confirm(`Are you sure about deleting "${customer.name}" customer?`)) {
            return;
        }

        deleteCustomer(customer.id).then(() => {
            handleRefresh.call({});
            toast.success(`Customer '${customer.name}' deleted successfully!`)
        })
    }

    /**
     * Update customer handler
     */
    function handleUpdate(value: MouseEvent<SVGSVGElement> | string | null = null) {
        if (loading) return;

        const newName = prompt(
            `Rename the '${customer.name}' customer`, typeof value === "string" && value.length > 0 ? value : customer.name
        );
        
        if (newName === null || newName === customer.name) {
            return;
        }

        if (newName.length < 3) {
            alert("The customer 'Name' must be at least 3 characters");
            handleUpdate(newName);
            return;
        }

        const data = {
            ...customer,
            name: newName
        }

        updateCustomer(data).then(() => {
            handleRefresh.call({})
            toast.success("Customer renamed successfully!");
        });
    }

    return (
        <div className='flex justify-around'>
            <button className="cursor-pointer text-gray-400 hover:text-blue-400"
                    disabled={loading}>
                <EditIcon onClick={handleUpdate}/>
            </button>

            <Link className="cursor-pointer text-gray-400 hover:text-green-600" href={`projects/${customer.id}`}>
                <EyeIcon/>
            </Link>

            <button className="cursor-pointer text-gray-400 hover:text-red-500"
                    disabled={loading}>
                <CrossIcon onClick={handleDelete}/>
            </button>
        </div>
    )
}

